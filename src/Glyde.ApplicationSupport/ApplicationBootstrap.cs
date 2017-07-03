using Glyde.ApplicationSupport.Bootstrapping;
using Glyde.ApplicationSupport.Internals;
using Glyde.Bootstrapper;
using Glyde.Configuration;
using Glyde.Configuration.Extensions;
using Glyde.Configuration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Glyde.Di;
using Glyde.Di.Builder;
using Microsoft.Extensions.DependencyModel;

namespace Glyde.ApplicationSupport
{
    public class ApplicationBootstrapper
        : IFluentAppBootstrappingConfiguration, IUseBootstrapping, IUseDependencyInjection, IUseConfiguration
    {
        private readonly IList<Assembly> _assemblies;        
        private IConfigurationLoader _configurationLoader;
        private readonly ContainerBuilder _bootstrappingContainerBuilder = new ContainerBuilder();

        public ApplicationBootstrapper()
        {
            var dependencyContext = DependencyContext.Load(Assembly.GetEntryAssembly());
            _assemblies = dependencyContext.RuntimeLibraries
                .SelectMany(l => l.GetDefaultAssemblyNames(dependencyContext).Select(Assembly.Load))
                .ToList();
        }

        public ApplicationBootstrapper(IEnumerable<Assembly> assemblies)
        {
            _assemblies = assemblies.ToList();
        }

        private IConfigurationService BuildConfigurationService()
        {
            var configurationSectionTypes =
                _assemblies.SelectMany(a => a.DefinedTypes.Where(t => t.IsConfigurationSection())).ToList();

            var applicationConfigurationModel = new ApplicationConfigurationModel(configurationSectionTypes);
            var configurationSections = _configurationLoader.Load(applicationConfigurationModel);

            var configurationService = new ConfigurationService(configurationSections);

            return configurationService;

        }

        public ApplicationBootstrapper ConfigureApplicationUsing<TConfigurationLoader>()
            where TConfigurationLoader : IConfigurationLoader, new()
        {
            _configurationLoader = new TConfigurationLoader();
            return this;
        }


        public ApplicationBootstrapper Use<TBootstrapperStage>() where TBootstrapperStage : class, IBootstrapperStage
        {
            _bootstrappingContainerBuilder.ForCollection<IBootstrapperStage>().Use<TBootstrapperStage>().AsTransient();
            return this;
        }

        public ApplicationBootstrapper ConfigureBootstrappingContainer(Action<IContainerBuilder> bootstrappingContainerExtender)
        {
            if (bootstrappingContainerExtender == null)
                throw new ArgumentNullException(nameof(bootstrappingContainerExtender));

            bootstrappingContainerExtender(_bootstrappingContainerBuilder);            

            return this;
        }

        public IGlydeApplication CreateApplication()
        {
            if (_configurationLoader == null)
                throw new InvalidOperationException("Configuration loader not set");

            var bootstrappingContainerConfiguration = CreateContainerConfiguration();
            var applicationContainerConfiguration = CreateContainerConfiguration();

            if (bootstrappingContainerConfiguration == null || applicationContainerConfiguration == null)
            {
                throw new InvalidOperationException("No Dependency Injection provider is set up.");
            }

            var bootstrappingContainer = bootstrappingContainerConfiguration.Container;
            var applicationContainer = applicationContainerConfiguration.Container;

            // register the application container within the bootstrapping container
            _bootstrappingContainerBuilder.For<IContainer>().Use(applicationContainer);

            // register a container builder instance that will be used to setup the application container
            var applicationContainerBuilder = new ContainerBuilder();
            _bootstrappingContainerBuilder.For<IContainerBuilder>().Use(applicationContainerBuilder);
            
            // register an application container configuration instance (needed only for DI stage)
            // _bootstrappingContainerBuilder.For<IContainerConfiguration>().Use(applicationContainerConfiguration);            

            // register configuration service within bootstrapper container.
            _bootstrappingContainerBuilder.For<IConfigurationService>().Use(BuildConfigurationService);

            // setup all services in bootstrapping container
            _bootstrappingContainerBuilder.Apply(bootstrappingContainerConfiguration);

            var bootstrapperStages = bootstrappingContainer.GetServices<IBootstrapperStage>();

            foreach (var stage in bootstrapperStages)
            {
                stage.RunStageBootstrappers(_assemblies);
            }

            // register glyde application instance within application container
            applicationContainerBuilder.For<IGlydeApplication>().Use<GlydeApplication>().AsSingleton();

            applicationContainerBuilder.Apply(applicationContainerConfiguration);

            return applicationContainer.GetService<IGlydeApplication>();
        }

        private IContainerConfiguration CreateContainerConfiguration()
        {
            return ((IUseDependencyInjection)this).ContainerConfigurationFactory
                ?.CreateContainerConfiguration();
        }

        IContainerConfigurationFactory IUseDependencyInjection.ContainerConfigurationFactory { get; set; }

        IUseBootstrapping IUseBootstrapping.RegisterBootstrapperStage(IBootstrapperStage bootstrapperStage)
        {
            _bootstrappingContainerBuilder.ForCollection<IBootstrapperStage>().Use(() => bootstrapperStage);
            return this;
        }

        IUseBootstrapping IUseBootstrapping.RegisterBootstrapperStage<TBootstrapperStage>()
        {
            Use<TBootstrapperStage>();
            return this;
        }
    }
}

