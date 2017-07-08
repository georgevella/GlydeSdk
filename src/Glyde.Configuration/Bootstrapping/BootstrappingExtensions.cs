using System;
using Glyde.Bootstrapper;
using Glyde.Configuration.Bootstrapping;

// ReSharper disable once CheckNamespace
namespace Glyde.Configuration
{
    public static class BootstrappingExtensions
    {
        public static T UseConfiguration<T>(this T bootstrapping, IConfigurationLoader configurationLoaderInstance)
            where T : IUseConfiguration
        {
            if (configurationLoaderInstance == null)
                throw new ArgumentNullException(nameof(configurationLoaderInstance));

            //bootstrapping.RegisterBootstrappingService(configurationLoaderInstance);
            bootstrapping.ConfigurationLoader = configurationLoaderInstance;
            bootstrapping.RegisterBootstrapperStage<ConfigurationBootstrappingStage>();

            return bootstrapping;
        }
    }
}