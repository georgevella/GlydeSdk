using System;
using Glyde.ApplicationSupport.Internals;
using Glyde.Bootstrapper;

namespace Glyde.ApplicationSupport
{
    public static class GlydeApplicationExtensions
    {
        public static void AssignServiceProvider(this IGlydeApplication glydeApplication, IServiceProvider serviceProviderInstance)
        {
            ((GlydeApplication)glydeApplication).ServiceProvider = serviceProviderInstance;
        }
    }
}