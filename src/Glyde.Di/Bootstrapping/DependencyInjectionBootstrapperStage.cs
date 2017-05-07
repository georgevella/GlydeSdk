using System;
using System.Collections.Generic;
using System.Reflection;
using Glyde.Bootstrapper;
using Glyde.Configuration;
using Glyde.Di.Builder;

namespace Glyde.Di.Bootstrapping
{
    public abstract class DependencyInjectionBootstrapperStage : BootstrapperStage<IDependencyInjectionBootstrapper>
    {
        public override void RunStageBootstrappers(IGlydeApplication app, IEnumerable<Assembly> assemblies)
        {
            var bootstrappers = GetBootstrappers(assemblies);
            var containerBuilder = new ContainerBuilder();

            containerBuilder.For<IServiceProvider>().Use(GetServiceProvider).AsSingleton();

            foreach (var bootstrapper in bootstrappers)
            {
                bootstrapper.RegisterServices(containerBuilder, ConfigurationService);
            }

            containerBuilder.Apply(CreateContainerConfiguration());
        }

        protected abstract IContainerConfiguration CreateContainerConfiguration();

        protected abstract IServiceProvider GetServiceProvider();

        public IConfigurationService ConfigurationService { get; set; }
    }
}