using System;
using System.Threading.Tasks;
using Glyde.ApplicationSupport.ApplicationStartup;
using Glyde.Bootstrapper;

namespace Glyde.ApplicationSupport.Internals
{
    internal class GlydeApplication : IGlydeApplication
    {
        public IServiceProvider ServiceProvider;

        public async Task<IApplicationStartupResult> Start()
        {
            var startupServiceFactory = (IStartupServiceFactory)ServiceProvider.GetService(typeof(IStartupServiceFactory));

            var startupServices = startupServiceFactory.GetAll();

            foreach (var runOnStartup in startupServices)
            {
                await runOnStartup.Run();
            }

            return new SuccessfulStartupResult();
        }
    }
}