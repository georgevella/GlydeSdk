using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Glyde.Bootstrapper;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;

namespace Glyde.AspNetCore.Bootstrapping
{
    public class AspNetCoreBootstrapperStage : IBootstrapperStage
    {
        private readonly ApplicationPartManager _applicationPartManager;
        private readonly IServiceCollection _services;
        private readonly Container _container;

        public AspNetCoreBootstrapperStage(ApplicationPartManager applicationPartManager, IServiceCollection services, Container container)
        {
            _applicationPartManager = applicationPartManager;
            _services = services;
            _container = container;
        }

        public void RunStageBootstrappers(IGlydeApplication app, IEnumerable<Assembly> assemblies)
        {
            var list = assemblies.ToList();

            // add all assemblies to application part manager, this will allow us to discover all controllers
            list.ForEach(assembly => _applicationPartManager.ApplicationParts.Add(new AssemblyPart(assembly)));

            // setup di integration with simpleinjector
            _services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(_container));
            _services.AddSingleton<IViewComponentActivator>(new SimpleInjectorViewComponentActivator(_container));
        }
    }
}