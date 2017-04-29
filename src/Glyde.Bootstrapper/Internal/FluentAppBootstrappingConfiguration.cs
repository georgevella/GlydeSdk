using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace Glyde.Bootstrapper.Internal
{
    internal class FluentAppBootstrappingConfiguration : IFluentAppBootstrappingConfiguration
    {
        internal List<IBootstrapperStage> BootstrappingStages = new List<IBootstrapperStage>();

        public IFluentAppBootstrappingConfiguration Using<TBootstrapperStage>() where TBootstrapperStage : IBootstrapperStage, new()
        {
            BootstrappingStages.Add(new TBootstrapperStage());
            return this;
        }

        public IFluentAppBootstrappingConfiguration Using(IBootstrapperStage bootstrapperStage)
        {
            BootstrappingStages.Add(bootstrapperStage);
            return this;
        }

        public void Run()
        {
            var dependencyContext = DependencyContext.Load(Assembly.GetEntryAssembly());
            var ownAssemblies = dependencyContext.RuntimeLibraries
                .SelectMany(l => l.GetDefaultAssemblyNames(dependencyContext).Select(Assembly.Load))
                .ToList();

            foreach (var stage in BootstrappingStages)
            {
                stage.Run(ownAssemblies);
            }
        }
    }
}