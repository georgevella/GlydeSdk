using Glyde.Bootstrapper;
using Glyde.Configuration;
using Glyde.Di.Builder;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Glyde.Di.Bootstrapping
{
    public abstract class DependencyInjectionBootstrapperStage : BootstrapperStage<IDependencyInjectionBootstrapper>
    {
        private readonly IConfigurationService _configurationService;

        protected DependencyInjectionBootstrapperStage(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public override void RunStageBootstrappers(IEnumerable<Assembly> assemblies)
        {
            var bootstrappers = GetBootstrappers(assemblies);
            var containerBuilder = new ContainerBuilder();

            containerBuilder.For<IServiceProvider>().Use(GetServiceProvider).AsSingleton();

            foreach (var bootstrapper in bootstrappers)
            {
                bootstrapper.RegisterServices(containerBuilder, _configurationService);
            }

            containerBuilder.Apply(CreateContainerConfiguration());
        }

        protected abstract IContainerConfiguration CreateContainerConfiguration();

        protected abstract IServiceProvider GetServiceProvider();
    }
}