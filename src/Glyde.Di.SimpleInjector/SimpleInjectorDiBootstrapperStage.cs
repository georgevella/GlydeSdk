using System;
using Glyde.Di.Bootstrapping;
using SimpleInjector;

namespace Glyde.Di.SimpleInjector
{
    public class SimpleInjectorDiBootstrapperStage : DependencyInjectionBootstrapperStage
    {
        private readonly IContainerConfiguration _configurationBuilder;

        public SimpleInjectorDiBootstrapperStage(Container container)
        {
            _configurationBuilder = new SimpleInjectorContainerConfiguration(container);
        }

        protected override IContainerConfiguration CreateContainerConfiguration()
        {
            return _configurationBuilder;
        }
    }
}