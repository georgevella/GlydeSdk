using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Glyde.ApplicationSupport;
using Glyde.AspNetCore.ApiExplorer;
using Glyde.AspNetCore.Bootstrapping;
using Glyde.AspNetCore.Controllers;
using Glyde.AspNetCore.Framework;
using Glyde.AspNetCore.Versioning;
using Glyde.Configuration.Loaders;
using Glyde.Web.Api.Controllers;
using Glyde.Web.Api.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Glyde.AspNetCore.Startup
{
    public static class MvcCoreBuilderExtensions
    {
        private const string ControllerVersionTemplate = "ControllerVersionTemplate";

        public static void BootstrapApi(this IMvcCoreBuilder mvcCoreBuilder, IEnumerable<Assembly> assemblies, IServiceCollection services, IConfigurationRoot configuration)
        {
            AspNetCoreApplication.Instance = new ApplicationBootstrapper(assemblies, AspNetCoreApplication.Container)
                .Using(new AspNetCoreBootstrapperStage(mvcCoreBuilder.PartManager, services, AspNetCoreApplication.Container))
                .ConfigureApplicationUsing<JsonConfigurationFileLoader>()
                .CreateApplication();

            var apiControllerMetadataProvider = new ApiControllerMetadataProvider();

            // register generic Restful API controller support
            mvcCoreBuilder.PartManager.FeatureProviders.Clear();
            mvcCoreBuilder.PartManager.FeatureProviders.Add(new ApiControllerFeatureProvider(apiControllerMetadataProvider));
            mvcCoreBuilder.PartManager.FeatureProviders.Add(new DefaultControllerFeatureProvider(apiControllerMetadataProvider));

            mvcCoreBuilder.AddApiExplorer();

            services.Configure<MvcOptions>(options =>
            {
                // we support only json for now
                var jsonFormatter = options.OutputFormatters.OfType<JsonOutputFormatter>().FirstOrDefault();
                //new JsonOutputFormatter(new JsonSerializerSettings(), ArrayPool<char>.Shared);
                if (jsonFormatter != null)
                {
                    options.OutputFormatters.Clear();
                    options.OutputFormatters.Add(jsonFormatter);
                }

                var template = configuration[ControllerVersionTemplate] ?? "api/v[version]";
                options.Conventions.Add(new ApiControllerConvention(template, apiControllerMetadataProvider, new ResourceMetadataProvider()));
                options.Conventions.Add(new DefaultControllerVersioningConvention(template));
                options.Conventions.Add(new ApiExplorerVisibilityEnabledConvention());
            });
        }

        public static void BootstrapApi(this IMvcCoreBuilder mvcCoreBuilder,
            Action<IGlydeWebApiConfiguration> configurator)
        {

        }
    }

    public interface IGlydeWebApiConfiguration
    {
        IGlydeWebApiConfiguration UseAssemblyList(IEnumerable<Assembly> assemblies);
        IGlydeWebApiConfiguration SetVersioningTemplate(string versioningTemplate);
        IGlydeWebApiConfiguration UseCustomJsonOutputSetings(JsonSerializerSettings jsonSerializerSettings);
        IGlydeWebApiConfiguration UseCustomJsonInputSetings(JsonSerializerSettings jsonSerializerSettings);

    }
}