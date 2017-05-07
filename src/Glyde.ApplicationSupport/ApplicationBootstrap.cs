using Glyde.ApplicationSupport.Internals;
using Glyde.Bootstrapper;

namespace Glyde.ApplicationSupport
{
    public static class BootstrapApplication
    {
        public static IFluentAppBootstrappingConfiguration Using<TBootstrapperStage>()
            where TBootstrapperStage : IBootstrapperStage, new()
        {
            return new FluentAppBootstrappingConfiguration()
            {
                BootstrappingStages =
                {
                    new TBootstrapperStage()
                }
            };
        }

        public static IFluentAppBootstrappingConfiguration Using(IBootstrapperStage bootstrapperStage)
        {
            return new FluentAppBootstrappingConfiguration()
            {
                BootstrappingStages =
                {
                    bootstrapperStage
                }
            };
        }
    }
}

