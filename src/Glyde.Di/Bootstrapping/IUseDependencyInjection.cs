using Glyde.Bootstrapper;

// ReSharper disable once CheckNamespace
namespace Glyde.Di
{
    public interface IUseDependencyInjection : IUseBootstrapping
    {
        IContainerConfigurationFactory ContainerConfigurationFactory {
            get;
            set;
        }
    }
}