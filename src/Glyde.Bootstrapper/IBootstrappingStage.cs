using System.Collections.Generic;
using System.Reflection;

namespace Glyde.Bootstrapper
{
    public interface IBootstrappingStage
    {
        void RunStageBootstrappers(IEnumerable<Assembly> assemblies);
    }
}