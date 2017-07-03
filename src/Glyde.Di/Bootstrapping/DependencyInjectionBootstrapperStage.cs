using Glyde.Bootstrapper;
using Glyde.Configuration;
using Glyde.Di.Builder;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Glyde.Di.Bootstrapping
{
    public class DependencyInjectionBootstrapperStage : BootstrapperStage<IDependencyInjectionBootstrapper>
    {
        private readonly IContainer _applicationContainer;
        private readonly IContainerBuilder _containerBuilder;
        private readonly IConfigurationService _configurationService;

        public DependencyInjectionBootstrapperStage(IContainer applicationContainer, IContainerBuilder containerBuilder, IConfigurationService configurationService)
        {
            _applicationContainer = applicationContainer;
            _containerBuilder = containerBuilder;
            _configurationService = configurationService;
        }

        public override void RunStageBootstrappers(IEnumerable<Assembly> assemblies)
        {
            var bootstrappers = GetBootstrappers(assemblies);

            foreach (var bootstrapper in bootstrappers)
            {
                bootstrapper.RegisterServices(_containerBuilder, _configurationService);
            }

            _containerBuilder.For<IServiceProvider>().Use(_applicationContainer);
            _containerBuilder.For<IContainer>().Use(_applicationContainer);            
        }
    }
}