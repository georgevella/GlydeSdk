using Glyde.ApplicationSupport.ApplicationStartup;
using Glyde.Bootstrapper;
using Glyde.Configuration;
using SimpleInjector;
using System.Threading.Tasks;

namespace Glyde.ApplicationSupport.Internals
{
    internal class GlydeApplication : IGlydeApplication
    {
        public readonly Container ApplicationContainer;

        public IConfigurationService ConfigurationService;

        public async Task<IApplicationStartupResult> Start()
        {
            var startupServiceFactory = ApplicationContainer.GetInstance<IStartupServiceFactory>();

            var startupServices = startupServiceFactory.GetAll();

            foreach (var runOnStartup in startupServices)
            {
                await runOnStartup.Run();
            }

            return new SuccessfulStartupResult();
        }

        public GlydeApplication(Container applicationContainer)
        {
            ApplicationContainer = applicationContainer;
        }
    }
}