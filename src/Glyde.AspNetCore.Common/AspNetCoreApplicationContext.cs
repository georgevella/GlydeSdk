using Glyde.ApplicationSupport;
using Glyde.Bootstrapper;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore;

namespace Glyde.AspNetCore
{
    internal static class AspNetCoreApplicationContext
    {
        public static IGlydeApplication Instance;        
    }
}