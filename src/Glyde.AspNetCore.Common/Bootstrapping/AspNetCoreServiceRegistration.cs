using Glyde.Configuration;
using Glyde.Di;
using Microsoft.AspNetCore.Http;

namespace Glyde.AspNetCore.Bootstrapping
{
    public class AspNetCoreServiceRegistration : IDependencyInjectionBootstrapper
    {
        public void RegisterServices(IContainerBuilder containerBuilder,
            IConfigurationService configurationService)
        {
            // register HTTP context accessor for those modules that need the http context
            containerBuilder.For<IHttpContextAccessor>().Use<HttpContextAccessor>().AsSingleton();

            //services.AddTransient<IApplicationModelProvider, GlydeApplicationModelProvider>();
        }
    }
}