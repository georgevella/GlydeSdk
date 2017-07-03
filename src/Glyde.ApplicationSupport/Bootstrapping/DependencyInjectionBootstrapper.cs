﻿using Glyde.ApplicationSupport.ApplicationStartup;
using Glyde.Configuration;
using Glyde.Di;

namespace Glyde.ApplicationSupport.Bootstrapping
{
    internal class DependencyInjectionBootstrapper : IDependencyInjectionBootstrapper
    {
        public void RegisterServices(IContainerBuilder containerBuilder, IConfigurationService configurationService)
        {
            containerBuilder.For<IStartupServiceFactory>().Use<StartupServiceFactory>();
            containerBuilder.For<IConfigurationService>().Use(configurationService);
        }
    }
}