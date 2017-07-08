// ReSharper disable once CheckNamespace

using Glyde.Bootstrapper;

namespace Glyde.Configuration
{
    public interface IUseConfiguration : IUseBootstrapping
    {
        IConfigurationLoader ConfigurationLoader { set; }
    }
}