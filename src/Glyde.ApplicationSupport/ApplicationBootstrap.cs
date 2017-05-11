using Glyde.ApplicationSupport.Bootstrapping;
using Glyde.ApplicationSupport.Internals;
using Glyde.Bootstrapper;
using Glyde.Configuration;
using Glyde.Configuration.Extensions;
using Glyde.Configuration.Models;
using Glyde.Di.SimpleInjector;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Glyde.ApplicationSupport
{
    public class ApplicationBootstrapper
        : IFluentAppBootstrappingConfiguration
    {
        private readonly IEnumerable<Assembly> _assemblies;
        private readonly Container _bootstrapperContainer = new Container();
        private IConfigurationLoader _configurationLoader;
        private readonly List<InstanceProducer<IBootstrapperStage>> _bootstrapperStageProducers
            = new List<InstanceProducer<IBootstrapperStage>>();

        private readonly GlydeApplication _application;

        public ApplicationBootstrapper(IEnumerable<Assembly> assemblies) : this(assemblies, new Container())
        {

        }

        public ApplicationBootstrapper(IEnumerable<Assembly> assemblies, Container applicationContainer)
        {
            _assemblies = assemblies;
            _application = new GlydeApplication(applicationContainer);
            _bootstrapperContainer.Register<IGlydeApplication>(() => _application, Lifestyle.Singleton);
            _bootstrapperContainer.Register<Container>(() => _application.ApplicationContainer);
            _bootstrapperContainer.Register<IConfigurationService>(BuildConfigurationService);

            Using<ApplicationConfigurationBootstrapperStage>();
            Using<SimpleInjectorDiBootstrapperStage>();
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

        public IFluentAppBootstrappingConfiguration ConfigureApplicationUsing<TConfigurationLoader>()
            where TConfigurationLoader : IConfigurationLoader, new()
        {
            _configurationLoader = new TConfigurationLoader();
            return this;
        }


        public IFluentAppBootstrappingConfiguration Using<TBootstrapperStage>() where TBootstrapperStage : class, IBootstrapperStage
        {
            _bootstrapperStageProducers.Add(Lifestyle.Transient.CreateProducer<IBootstrapperStage, TBootstrapperStage>(_bootstrapperContainer));
            return this;
        }

        public IFluentAppBootstrappingConfiguration Using(IBootstrapperStage bootstrapperStage)
        {
            _bootstrapperStageProducers.Add(Lifestyle.Transient.CreateProducer<IBootstrapperStage>(() => bootstrapperStage, _bootstrapperContainer));

            return this;
        }

        public IGlydeApplication CreateApplication()
        {
            if (_configurationLoader == null)
                throw new InvalidOperationException("Configuration loader not set");

            foreach (var stageProducer in _bootstrapperStageProducers)
            {
                var stage = stageProducer.GetInstance();
                stage.RunStageBootstrappers(_assemblies);
            }

            return _application;
        }
    }
}

