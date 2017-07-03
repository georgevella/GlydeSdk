using System;
using SimpleInjector;

namespace Glyde.Di.SimpleInjector
{
    public class SimpleInjectorContainerConfigurationFactory : IContainerConfigurationFactory
    {
        private readonly Action<Container> _containerConfigurator = c => { };

        public SimpleInjectorContainerConfigurationFactory()
        {
            
        }

        public SimpleInjectorContainerConfigurationFactory(Action<Container> containerConfigurator)
        {
            _containerConfigurator = containerConfigurator;
        }

        public IContainerConfiguration CreateContainerConfiguration()
        {
            var container = new Container();
            _containerConfigurator(container);
            return new SimpleInjectorContainerConfiguration(container);
        }
    }
}