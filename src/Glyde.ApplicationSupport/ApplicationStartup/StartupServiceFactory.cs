using System.Collections.Generic;
using System.Linq;

namespace Glyde.ApplicationSupport.ApplicationStartup
{
    class StartupServiceFactory : IStartupServiceFactory
    {
        private readonly IEnumerable<IRunOnStartup> _startupServices;

        public StartupServiceFactory(IEnumerable<IRunOnStartup> startupServices)
        {
            _startupServices = startupServices;
        }
        public IEnumerable<IRunOnStartup> GetAll()
        {
            if (_startupServices == null)
                return Enumerable.Empty<IRunOnStartup>();

            return _startupServices;
        }
    }
}