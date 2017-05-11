using System.Collections.Generic;
using System.Reflection;

namespace Glyde.Bootstrapper
{
    public interface IBootstrapperStage
    {
        void RunStageBootstrappers(IEnumerable<Assembly> assemblies);
    }
}