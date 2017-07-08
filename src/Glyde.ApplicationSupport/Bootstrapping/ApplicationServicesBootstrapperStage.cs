using Glyde.ApplicationSupport.ApplicationConfiguration;
using Glyde.ApplicationSupport.ApplicationStartup;
using Glyde.ApplicationSupport.Internals;
using Glyde.Bootstrapper;
using Glyde.Configuration;
using Glyde.Configuration.Extensions;
using Glyde.Configuration.Loaders;
using Glyde.Configuration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Glyde.Di;

namespace Glyde.ApplicationSupport.Bootstrapping
{
    public class ApplicationServicesBootstrapperStage : BaseBootstrappingStage<IApplicationServicesBootstrapper>
    {
        private readonly IContainerBuilder _containerBuilder;
        private readonly IConfigurationService _configurationService;

        public ApplicationServicesBootstrapperStage(IContainerBuilder containerBuilder, IConfigurationService configurationService)
        {
            _containerBuilder = containerBuilder;
            _configurationService = configurationService;
        }

        public override void RunStageBootstrappers(IEnumerable<Assembly> assemblies)
        {
            var bootstrappers = GetBootstrappers(assemblies);

            var startupServices = new List<Type>();
            var builder = new ApplicationConfigurationBuilder(startupServices);

            foreach (var bootstrapper in bootstrappers)
            {
                bootstrapper.RegisterApplicationServices(builder, _configurationService);
            }

            startupServices.ForEach(t => _containerBuilder.ForCollection<IRunOnStartup>().Use(t).AsTransient());            
        }        
    }
}