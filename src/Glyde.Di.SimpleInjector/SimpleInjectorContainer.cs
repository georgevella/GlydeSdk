using System;
using System.Collections.Generic;
using SimpleInjector;

namespace Glyde.Di.SimpleInjector
{
    public class SimpleInjectorContainer : IContainer
    {
        internal Container Container { get; }
        private readonly SimpleInjectorContainerConfiguration _containerConfiguration;

        internal SimpleInjectorContainer(Container container, SimpleInjectorContainerConfiguration containerConfiguration)
        {
            Container = container;
            _containerConfiguration = containerConfiguration;
        }

        public object GetService(Type serviceType)
        {
            if (!_containerConfiguration.ContainsRegistrations)
                throw new InvalidOperationException("Container not yet setup");

            return ((IServiceProvider) Container).GetService(serviceType);
        }

        public T GetService<T>() where T: class
        {
            if (!_containerConfiguration.ContainsRegistrations)
                throw new InvalidOperationException("Container not yet setup");

            return Container.GetInstance<T>();
        }

        public IEnumerable<T> GetServices<T>() where T : class
        {
            if (!_containerConfiguration.ContainsRegistrations)
                throw new InvalidOperationException("Container not yet setup");

            return Container.GetAllInstances<T>();
        }
    }
}