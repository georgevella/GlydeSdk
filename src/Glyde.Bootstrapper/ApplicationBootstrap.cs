using System.Reflection;
using Glyde.Bootstrapper.Internal;
using Microsoft.Extensions.DependencyModel;

namespace Glyde.Bootstrapper
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

    public interface IFluentAppBootstrappingConfiguration
    {
        IFluentAppBootstrappingConfiguration Using<TBootstrapperStage>()
            where TBootstrapperStage : IBootstrapperStage, new();

        IFluentAppBootstrappingConfiguration Using(IBootstrapperStage bootstrapperStage);
        void Run();
    }
}