using System.Collections.Generic;

namespace Glyde.ApplicationSupport.ApplicationStartup
{
    public interface IStartupServiceFactory
    {
        IEnumerable<IRunOnStartup> GetAll();
    }
}