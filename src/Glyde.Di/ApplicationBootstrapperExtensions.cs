using System;
using Glyde.Bootstrapper;
using Glyde.Di.Bootstrapping;

namespace Glyde.Di
{
    public static class ApplicationBootstrapperExtensions
    {
        public static T UseDependencyInjection<T>(this T applicationBootstrapper, IContainerConfigurationFactory containerConfigurationFactory)
            where T : IUseDependencyInjection
        {
            if (containerConfigurationFactory == null)
                throw new ArgumentNullException(nameof(containerConfigurationFactory));

            applicationBootstrapper.RegisterBootstrapperStage<DependencyInjectionBootstrapperStage>();
            applicationBootstrapper.ContainerConfigurationFactory = containerConfigurationFactory;

            return applicationBootstrapper;
        }

        public static T UseDependencyInjection<T, TContainerConfigurationFactory>(this T applicationBootstrapper)
            where T : IUseDependencyInjection
            where TContainerConfigurationFactory : IContainerConfigurationFactory, new()
        {
            applicationBootstrapper.RegisterBootstrapperStage<DependencyInjectionBootstrapperStage>();
            applicationBootstrapper.ContainerConfigurationFactory = new TContainerConfigurationFactory();

            return applicationBootstrapper;
        }
    }
}