using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Glyde.ApplicationSupport.ApplicationConfiguration;
using Glyde.ApplicationSupport.ApplicationStartup;
using Glyde.Bootstrapper;
using Glyde.Configuration;
using Glyde.Di;
using SimpleInjector;

namespace Glyde.ApplicationSupport.Bootstrapping
{
    public class ApplicationConfigurationBootstrapperStage : BootstrapperStage<IApplicationConfigurationBootstrapper>
    {
        private readonly Container _container;

        public ApplicationConfigurationBootstrapperStage(Container container)
        {
            _container = container;
        }

        public override void RunStageBootstrappers(IGlydeApplication app, IEnumerable<Assembly> assemblies)
        {
            var bootstrappers = GetBootstrappers(assemblies);

            var startupServices = new List<Type>();
            var builder = new ApplicationConfigurationBuilder(startupServices);

            foreach (var bootstrapper in bootstrappers)
            {
                bootstrapper.RegisterApplicationStartupService(builder, ConfigurationService);
            }

            var registrations = startupServices.Select(t => Lifestyle.Transient.CreateRegistration(t, _container));
            _container.RegisterCollection<IRunOnStartup>(registrations);

            app.AssignServiceProvider(_container);
        }

        public IConfigurationService ConfigurationService { get; set; }
    }
}