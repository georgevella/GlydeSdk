using Glyde.Bootstrapper;

namespace Glyde.ApplicationSupport
{
    public interface IFluentAppBootstrappingConfiguration
    {
        IFluentAppBootstrappingConfiguration Using<TBootstrapperStage>()
            where TBootstrapperStage : IBootstrapperStage, new();

        IFluentAppBootstrappingConfiguration Using(IBootstrapperStage bootstrapperStage);
        IGlydeApplication CreateApplication();
    }
}