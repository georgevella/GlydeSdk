using System.Collections.Generic;
using System.Reflection;
using Glyde.Bootstrapper;
using Glyde.Configuration;
using Glyde.Di.Builder;

namespace Glyde.Di.Bootstrapping
{
    public abstract class DependencyInjectionBootstrapperStage : BootstrapperStage<IDependencyInjectionBootstrapper>
    {
        public override void Run(IEnumerable<Assembly> assemblies)
        {
            var bootstrappers = GetBootstrappers(assemblies);
            var containerBuilder = new ContainerBuilder();

            foreach (var bootstrapper in bootstrappers)
            {
                bootstrapper.RegisterServices(containerBuilder, ConfigurationService);
            }            

            containerBuilder.Apply(CreateContainerConfiguration());
        }

        protected abstract IContainerConfiguration CreateContainerConfiguration();

        public IConfigurationService ConfigurationService { get; set; }
    }
}