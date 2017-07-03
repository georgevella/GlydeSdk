using System;
using SimpleInjector;

namespace Glyde.Di.SimpleInjector
{
    public static class ContainerExtensions
    {
        public static Container AsSimpleInjectorContainer(this IContainer container)
        {
            var wrappingContainer = container as SimpleInjectorContainer;

            if (wrappingContainer == null)
                throw new InvalidOperationException();

            return wrappingContainer.Container;
        }
    }
}