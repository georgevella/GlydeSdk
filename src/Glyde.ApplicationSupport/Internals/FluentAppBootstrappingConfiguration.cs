using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Glyde.Bootstrapper;
using Microsoft.Extensions.DependencyModel;

namespace Glyde.ApplicationSupport.Internals
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

        public IGlydeApplication CreateApplication()
        {
            var dependencyContext = DependencyContext.Load(Assembly.GetEntryAssembly());
            var ownAssemblies = dependencyContext.RuntimeLibraries
                .SelectMany(l => l.GetDefaultAssemblyNames(dependencyContext).Select(Assembly.Load))
                .ToList();

            var app = new GlydeApplication();

            foreach (var stage in BootstrappingStages)
            {
                stage.RunStageBootstrappers(app, ownAssemblies);
            }

            return app;
        }
    }
}