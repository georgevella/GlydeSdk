using Glyde.ApplicationSupport.ApplicationConfiguration;
using Glyde.ApplicationSupport.Bootstrapping;
using Glyde.Configuration;
using Glyde.Di;
using Glyde.Di.Bootstrapping;
using GlydeSdk.Sample.SimpleConsoleApp.Services;

namespace GlydeSdk.Sample.SimpleConsoleApp.Bootstrapping
{
    public class DefaultBootstrapper : IDependencyInjectionBootstrapper, IApplicationServicesBootstrapper
    {
        public void RegisterServices(IContainerBuilder containerBuilder, IConfigurationService configurationService)
        {
            containerBuilder.For<IService>().Use<Service>();
        }

        public void RegisterApplicationServices(IApplicationConfigurationBuilder applicationConfigurationBuilder,
            IConfigurationService configurationService)
        {
            applicationConfigurationBuilder.RegisterStartupService<TestStartupService>();
        }
    }
}