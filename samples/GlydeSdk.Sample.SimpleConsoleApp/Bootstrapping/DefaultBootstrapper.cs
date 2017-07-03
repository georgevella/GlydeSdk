using Glyde.ApplicationSupport.Bootstrapping;
using Glyde.Configuration;
using Glyde.Di;
using GlydeSdk.Sample.SimpleConsoleApp.Services;

namespace GlydeSdk.Sample.SimpleConsoleApp.Bootstrapping
{
    public class DefaultBootstrapper : IDependencyInjectionBootstrapper
    {
        public void RegisterServices(IContainerBuilder containerBuilder, IConfigurationService configurationService)
        {
            containerBuilder.For<IService>().Use<Service>();
        }
    }
}