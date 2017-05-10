using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Glyde.Bootstrapper;
using Glyde.Configuration.Models;

namespace Glyde.Configuration.Bootstrapping
{
    public class ConfigurationBootstrapperStage : IBootstrapperStage
    {
        /// <inheritdoc />
        public void RunStageBootstrappers(IGlydeApplication app, IEnumerable<Assembly> assemblies)
        {
            var configurationSectionTypes = assemblies.SelectMany(a => a.DefinedTypes.Where(t => IsConfigurationSection(t.AsType()))).ToList();


        }

        private bool IsConfigurationSection(Type type)
        {
            var typeinfo = type.GetTypeInfo();
            if (typeinfo.IsAbstract)
                return false;

            return InheritsFromConfigurationSection(type);
        }

        private bool InheritsFromConfigurationSection(Type type)
        {
            if (type == typeof(object))
                return false;

            if (type == typeof(ConfigurationSection))
                return true;

            var typeinfo = type.GetTypeInfo();

            return InheritsFromConfigurationSection(typeinfo.BaseType);
        }
    }
}