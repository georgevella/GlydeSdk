using Glyde.ApplicationSupport.ApplicationConfiguration;
using Glyde.ApplicationSupport.ApplicationStartup;
using Glyde.ApplicationSupport.Internals;
using Glyde.Bootstrapper;
using Glyde.Configuration;
using Glyde.Configuration.Extensions;
using Glyde.Configuration.Loaders;
using Glyde.Configuration.Models;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Glyde.ApplicationSupport.Bootstrapping
{
    public class ApplicationConfigurationBootstrapperStage : BootstrapperStage<IApplicationConfigurationBootstrapper>
    {
        private readonly GlydeApplication _glydeApplication;
        private readonly Container _container;

        public ApplicationConfigurationBootstrapperStage(IGlydeApplication glydeApplication, Container container)
        {
            _glydeApplication = (GlydeApplication)glydeApplication;
            _container = container;
        }

        public override void RunStageBootstrappers(IEnumerable<Assembly> assemblies)
        {
            var bootstrappers = GetBootstrappers(assemblies);

            var startupServices = new List<Type>();
            var builder = new ApplicationConfigurationBuilder(startupServices);

            foreach (var bootstrapper in bootstrappers)
            {
                bootstrapper.RegisterApplicationServices(builder, ConfigurationService);
            }

            var registrations = startupServices.Select(t => Lifestyle.Transient.CreateRegistration(t, _container));
            _container.RegisterCollection<IRunOnStartup>(registrations);
        }

        public IConfigurationService ConfigurationService { get; set; }
    }
}