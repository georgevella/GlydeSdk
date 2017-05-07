using Glyde.Bootstrapper;
using Glyde.Configuration;

namespace Glyde.ApplicationSupport.ApplicationConfiguration
{
    public interface IApplicationConfigurationBootstrapper : IBootstrapper
    {
        void RegisterApplicationStartupService(IApplicationConfigurationBuilder applicationConfigurationBuilder, IConfigurationService configurationService);
    }
}