using Glyde.Bootstrapper;
using Glyde.Configuration;

namespace Glyde.ApplicationSupport
{
    public interface IFluentAppBootstrappingConfiguration
    {
        IFluentAppBootstrappingConfiguration Using<TBootstrapperStage>()
            where TBootstrapperStage : class, IBootstrapperStage;

        IFluentAppBootstrappingConfiguration Using(IBootstrapperStage bootstrapperStage);
        IGlydeApplication CreateApplication();

        IFluentAppBootstrappingConfiguration ConfigureApplicationUsing<TConfigurationLoader>()
            where TConfigurationLoader : IConfigurationLoader, new();
    }
}