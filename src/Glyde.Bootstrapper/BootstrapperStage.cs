using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Glyde.Bootstrapper.Internal;

namespace Glyde.Bootstrapper
{
    /// <summary>
    ///     Base class implemented by all bootstrapper stages (configuration, dependency injection, etc)
    /// </summary>
    /// <typeparam name="TBootstrapperContract"></typeparam>
    public abstract class BootstrapperStage<TBootstrapperContract> 
        : IBootstrapperStage where TBootstrapperContract : IBootstrapper
    {
        public abstract void Run(IEnumerable<Assembly> assemblies);

        public IEnumerable<TBootstrapperContract> GetBootstrappers(IEnumerable<Assembly> assemblies)
        {
            var result = new List<TBootstrapperContract>();
            var bootstrapperContractType = typeof(TBootstrapperContract);
            foreach (var assembly in assemblies)
            {
                var assemblyBootstrappers = assembly.DefinedTypes
                    .Where(t => t.IsClass && !t.IsAbstract && t.ImplementedInterfaces.Contains(bootstrapperContractType))
                    .Select(t => (TBootstrapperContract)BootstrapperCache.Get(t.AsType()))
                    .ToList();

                result.AddRange(assemblyBootstrappers);
            }

            return result;
        }
    }
}