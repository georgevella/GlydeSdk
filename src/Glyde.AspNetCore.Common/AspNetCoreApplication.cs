using Glyde.Bootstrapper;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore;

namespace Glyde.AspNetCore
{
    internal static class AspNetCoreApplication
    {
        public static IGlydeApplication Instance;

        public static readonly Container Container = new Container();


        static AspNetCoreApplication()
        {
            Container.Options.DefaultScopedLifestyle = new AspNetRequestLifestyle();

        }
    }
}