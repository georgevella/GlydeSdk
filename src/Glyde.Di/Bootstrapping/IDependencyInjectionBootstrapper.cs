using Glyde.Bootstrapper;
using Glyde.Configuration;

namespace Glyde.Di.Bootstrapping
{
    /// <summary>
    ///     Contract used by bootstrappers that register services with the DI container.
    /// </summary>
    public interface IDependencyInjectionBootstrapper : IBootstrapper
    {
        void RegisterServices(IContainerBuilder containerBuilder, IConfigurationService configurationService);
    }
}