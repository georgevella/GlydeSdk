using Glyde.Configuration;
using Glyde.Di.Bootstrapping;
using SimpleInjector;
using System;

namespace Glyde.Di.SimpleInjector
{
    public class SimpleInjectorDiBootstrapperStage : DependencyInjectionBootstrapperStage
    {
        private readonly Container _container;
        private readonly IContainerConfiguration _configurationBuilder;

        public SimpleInjectorDiBootstrapperStage(Container container, IConfigurationService configurationService)
            : base(configurationService)
        {
            _container = container;
            _configurationBuilder = new SimpleInjectorContainerConfiguration(container);
        }

        protected override IContainerConfiguration CreateContainerConfiguration()
        {
            return _configurationBuilder;
        }

        protected override IServiceProvider GetServiceProvider()
        {
            return _container;
        }
    }
}