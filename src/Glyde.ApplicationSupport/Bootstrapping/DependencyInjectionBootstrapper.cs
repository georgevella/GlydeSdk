using Glyde.ApplicationSupport.ApplicationStartup;
using Glyde.Configuration;
using Glyde.Di;
using Glyde.Di.Bootstrapping;

namespace Glyde.ApplicationSupport.Bootstrapping
{
    internal class DependencyInjectionBootstrapper : IDependencyInjectionBootstrapper
    {
        public void RegisterServices(IContainerBuilder containerBuilder, IConfigurationService configurationService)
        {
            containerBuilder.For<IStartupServiceFactory>().Use<StartupServiceFactory>();
            // start an empty collection registration of IRunOnStartup services
            containerBuilder.ForCollection<IRunOnStartup>();

            containerBuilder.For<IConfigurationService>().Use(configurationService);
        }
    }
}