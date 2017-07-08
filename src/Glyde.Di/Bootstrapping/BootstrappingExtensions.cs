using System;
using Glyde.Di.Bootstrapping;

// ReSharper disable once CheckNamespace
namespace Glyde.Di
{
    public static class BootstrappingExtensions
    {
        /// <summary>
        ///     Registers the bootstrapping stage required to setup dependency injection within the application.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bootstrapping"></param>
        /// <param name="containerConfigurationFactory"></param>
        /// <returns></returns>
        public static T UseDependencyInjection<T>(this T bootstrapping, IContainerConfigurationFactory containerConfigurationFactory)
            where T : IUseDependencyInjection
        {
            if (containerConfigurationFactory == null)
                throw new ArgumentNullException(nameof(containerConfigurationFactory));

            bootstrapping.RegisterBootstrapperStage<DependencyInjectionBootstrappingStage>();
            bootstrapping.ContainerConfigurationFactory = containerConfigurationFactory;

            return bootstrapping;
        }

        /// <summary>
        ///     Registers the bootstrapping stage required to setup dependency injection within the application.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TContainerConfigurationFactory"></typeparam>
        /// <param name="applicationBootstrapper"></param>
        /// <returns></returns>
        public static T UseDependencyInjection<T, TContainerConfigurationFactory>(this T applicationBootstrapper)
            where T : IUseDependencyInjection
            where TContainerConfigurationFactory : IContainerConfigurationFactory, new()
        {
            applicationBootstrapper.RegisterBootstrapperStage<DependencyInjectionBootstrappingStage>();
            applicationBootstrapper.ContainerConfigurationFactory = new TContainerConfigurationFactory();

            return applicationBootstrapper;
        }
    }
}