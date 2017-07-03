using System;
using System.Globalization;
using System.Linq;
using Glyde.AspNetCore.ApiExplorer;
using Glyde.AspNetCore.Controllers;
using Glyde.AspNetCore.Framework;
using Glyde.AspNetCore.Versioning;
using Glyde.Web.Api.Controllers;
using Glyde.Web.Api.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Formatters.Json.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            services.AddMvcCore()
                .AddJsonFormatters(settings =>
                {
                    settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    settings.Formatting = Formatting.Indented;
                    settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                })
                .BootstrapApi(GetAssemblies(), services, Configuration);
        }
    }
}