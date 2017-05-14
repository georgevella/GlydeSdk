using Glyde.Configuration;
using Glyde.Di;
using Glyde.Web.Api.Client;
using Glyde.Web.Api.Resources;

namespace Glyde.Web.Api.Bootstrapping
{
    public class DependencyInjectionBootstrapper : IDependencyInjectionBootstrapper
    {
        public void RegisterServices(IContainerBuilder containerBuilder, IConfigurationService configurationService)
        {
            containerBuilder.For<IHttpClientFactory>().Use<HttpClientFactory>().AsSingleton();
            containerBuilder.For<IResourceMetadataProvider>().Use<ResourceMetadataProvider>().AsSingleton();
            containerBuilder.For<IApiClientFactory>().Use<ApiClientFactory>().AsSingleton();
        }
    }
}