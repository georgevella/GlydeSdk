using System;
using System.Collections.Generic;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;

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

        public IScope StartScope()
        {
            return new Scope(Container);
        }
    }

    internal class Scope : IScope
    {
        private global::SimpleInjector.Scope _scope;

        public Scope(Container container)
        {
            var container1 = container ?? throw new ArgumentNullException(nameof(container));
            _scope = container1.BeginExecutionContextScope();
        }


        public void Dispose()
        {
            _scope?.Dispose();
            _scope = null;
        }
    }
}