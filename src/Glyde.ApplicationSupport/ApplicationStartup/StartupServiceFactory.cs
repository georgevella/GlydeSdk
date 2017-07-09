using System.Collections.Generic;
using System.Linq;

namespace Glyde.ApplicationSupport.ApplicationStartup
{
    internal class StartupServiceFactory : IStartupServiceFactory
    {
        private readonly IEnumerable<IRunOnStartup> _startupServices;

        public StartupServiceFactory(IEnumerable<IRunOnStartup> startupServices = null)
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