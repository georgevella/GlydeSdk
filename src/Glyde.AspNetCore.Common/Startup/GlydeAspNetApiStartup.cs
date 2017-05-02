using System;
using System.Globalization;
using System.Linq;
using Glyde.AspNetCore.ApiExplorer;
using Glyde.AspNetCore.Controllers;
using Glyde.AspNetCore.Framework;
using Glyde.AspNetCore.Versioning;
using Glyde.Web.Api.Controllers;
using Glyde.Web.Api.Resources;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Formatters.Json.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Utilities;

namespace Glyde.AspNetCore.Startup
{
    public class GlydeAspNetApiStartup : CommonGlydeAspNetStartup
    {
        private const string ControllerVersionTemplate = "ControllerVersionTemplate";
        // This method gets called by the runtime. Use this method to add services to the container.
        public GlydeAspNetApiStartup(IHostingEnvironment env) : base(env)
        {

        }

        public void ConfigureServices(IServiceCollection services)
        {            
            var mvcBuilder = services.AddMvcCore()
                .AddJsonFormatters(settings =>
                {
                    settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    settings.Formatting = Formatting.Indented;
                    settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                });

            ConfigureGlydeServices(mvcBuilder.PartManager, services);

            var apiControllerMetadataProvider = new ApiControllerMetadataProvider();

            // register generic Restful API controller support
            mvcBuilder.PartManager.FeatureProviders.Clear();
            mvcBuilder.PartManager.FeatureProviders.Add(new ApiControllerFeatureProvider(apiControllerMetadataProvider));
            mvcBuilder.PartManager.FeatureProviders.Add(new DefaultControllerFeatureProvider(apiControllerMetadataProvider));

            mvcBuilder.AddApiExplorer();

            services.Configure<MvcOptions>(options =>
            {
                // we support only json for now
                var jsonFormatter = options.OutputFormatters.OfType<JsonOutputFormatter>().FirstOrDefault();
                if (jsonFormatter != null)
                {
                    options.OutputFormatters.Clear();
                    options.OutputFormatters.Add(jsonFormatter);
                }

                var template = Configuration[ControllerVersionTemplate] ?? "api/v[version]";
                options.Conventions.Add(new ApiControllerConvention(template, apiControllerMetadataProvider, new ResourceMetadataProvider()));
                options.Conventions.Add(new DefaultControllerVersioningConvention(template));
                options.Conventions.Add(new ApiExplorerVisibilityEnabledConvention());
            });
        }
    }    
}