using Glyde.ApplicationSupport.ApplicationStartup;
using Glyde.Bootstrapper;
using Glyde.Configuration;
using System.Threading.Tasks;
using Glyde.Di;

namespace Glyde.ApplicationSupport.Internals
{
    internal class GlydeApplication : IGlydeApplication
    {
        public IConfigurationService ConfigurationService;

        public IContainer Container { get; }

        public async Task<IApplicationStartupResult> Start()
        {
            var startupServiceFactory = Container.GetService<IStartupServiceFactory>();

            var startupServices = startupServiceFactory.GetAll();

            foreach (var runOnStartup in startupServices)
            {
                await runOnStartup.Run();
            }

            return new SuccessfulStartupResult();
        }

        public GlydeApplication(IContainer applicationContainer)
        {
            Container = applicationContainer;
        }
    }
}