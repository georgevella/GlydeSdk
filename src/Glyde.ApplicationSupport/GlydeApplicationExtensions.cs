using System;
using Glyde.ApplicationSupport.Internals;
using Glyde.Bootstrapper;
using Glyde.Configuration;

namespace Glyde.ApplicationSupport
{
    public static class GlydeApplicationExtensions
    {
        public static void AssignServiceProvider(this IGlydeApplication glydeApplication, IServiceProvider serviceProviderInstance)
        {
            ((GlydeApplication)glydeApplication).ServiceProvider = serviceProviderInstance;
        }
        public static void AssignConfigurationService(this IGlydeApplication glydeApplication, IConfigurationService configurationService)
        {
            ((GlydeApplication)glydeApplication).ConfigurationService = configurationService;
        }
    }
}