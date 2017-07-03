using Glyde.Di;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Glyde.AspNetCore.Framework
{
    public class ControllerActivator : IControllerActivator
    {
        private readonly IContainer _container;

        public ControllerActivator(IContainer container)
        {
            _container = container;
        }

        public object Create(ControllerContext context)
        {
            return _container.GetService(context.ActionDescriptor.ControllerTypeInfo.AsType());

        }

        public void Release(ControllerContext context, object controller)
        {
            
        }
    }
}