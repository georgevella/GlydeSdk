using Glyde.Bootstrapper;
using Glyde.Configuration;

namespace Glyde.ApplicationSupport.ApplicationConfiguration
{
    public interface IApplicationConfigurationBootstrapper : IBootstrapper
    {
        void RegisterApplicationServices(IApplicationConfigurationBuilder applicationConfigurationBuilder, IConfigurationService configurationService);
    }
}