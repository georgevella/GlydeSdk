﻿using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Glyde.ApplicationSupport;
using Glyde.ApplicationSupport.Extensions;
using Glyde.AspNetCore.ApiExplorer;
using Glyde.AspNetCore.Bootstrapping;
using Glyde.AspNetCore.Controllers;
using Glyde.AspNetCore.Framework;
using Glyde.AspNetCore.Versioning;
using Glyde.Configuration;
using Glyde.Configuration.Loaders;
using Glyde.Di;
using Glyde.Di.SimpleInjector;
using Glyde.Web.Api.Controllers;
using Glyde.Web.Api.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SimpleInjector.Integration.AspNetCore;

namespace Glyde.AspNetCore.Startup
{
    public static class MvcBuilderExtensions
    {
        private const string ControllerVersionTemplate = "ControllerVersionTemplate";

        public static void BootstrapApi(this IMvcCoreBuilder mvcCoreBuilder, IEnumerable<Assembly> assemblies, IConfigurationRoot configuration)
        {
            Bootstrap(mvcCoreBuilder.PartManager, assemblies, mvcCoreBuilder.Services);

            var apiControllerMetadataProvider = new ApiControllerMetadataProvider();

            // register generic Restful API controller support
            mvcCoreBuilder.PartManager.FeatureProviders.Clear();
            mvcCoreBuilder.PartManager.FeatureProviders.Add(new ApiControllerFeatureProvider(apiControllerMetadataProvider));
            mvcCoreBuilder.PartManager.FeatureProviders.Add(new DefaultControllerFeatureProvider(apiControllerMetadataProvider));

            mvcCoreBuilder.AddApiExplorer();

            mvcCoreBuilder.Services.Configure<MvcOptions>(options =>
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
        public static void BootstrapMvc(this IMvcBuilder mvcBuilder, IEnumerable<Assembly> assemblies, IConfigurationRoot configuration)
        {
            Bootstrap(mvcBuilder.PartManager, assemblies, mvcBuilder.Services);

            mvcBuilder.Services.Configure<MvcOptions>(options =>
            {
                // we support only json for now
                var jsonFormatter = options.OutputFormatters.OfType<JsonOutputFormatter>().FirstOrDefault();
                //new JsonOutputFormatter(new JsonSerializerSettings(), ArrayPool<char>.Shared);
                if (jsonFormatter != null)
                {
                    options.OutputFormatters.Clear();
                    options.OutputFormatters.Add(jsonFormatter);
                }
            });
        }

        internal static void Bootstrap(ApplicationPartManager partManager, IEnumerable<Assembly> assemblies, IServiceCollection services)
        {
            var assemblyList = assemblies as IList<Assembly> ?? assemblies.ToList();
            assemblyList.ToList().ForEach(assembly => partManager.ApplicationParts.Add(new AssemblyPart(assembly)));            

            AspNetCoreApplicationContext.Instance = new ApplicationBootstrapper(assemblyList)
                .UseApplicationServices()
                .UseDependencyInjection(
                    new SimpleInjectorContainerConfigurationFactory(c =>
                    {
                        c.Options.DefaultScopedLifestyle = new AspNetRequestLifestyle();
                    })
                )
                .UseConfiguration(new JsonConfigurationFileLoader())
                .CreateApplication();

            // register IHttpContextAccessor within ASP.NET framework DI
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IControllerActivator>(new ControllerActivator(AspNetCoreApplicationContext.Instance.Container));
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