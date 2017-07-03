using Glyde.Bootstrapper;

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