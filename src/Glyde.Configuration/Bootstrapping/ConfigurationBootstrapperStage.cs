using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Glyde.Bootstrapper;
using Glyde.Configuration.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Glyde.Configuration.Bootstrapping
{
    public class ConfigurationBootstrapperStage : IBootstrapperStage
    {
        /// <inheritdoc />
        public void RunStageBootstrappers(IGlydeApplication app, IEnumerable<Assembly> assemblies)
        {
            var configurationSectionTypes = assemblies.SelectMany(a => a.DefinedTypes.Where(t => IsConfigurationSection(t.AsType()))).ToList();

            var configurationSectionMap = configurationSectionTypes.Select(BuildMapping)
                .ToDictionary(x => x.name, x => x.typeInfo);

            var configurationSections = new Dictionary<Type, ConfigurationSection>();

            using (var json = new StreamReader(File.OpenRead("config.json")))
            using (var reader = new JsonTextReader(json))
            {
                var serializer = JsonSerializer.Create(new JsonSerializerSettings()
                {

                });

                var jobj = serializer.Deserialize<Dictionary<string, JObject>>(reader);

                foreach (var prop in jobj)
                {
                    if (!configurationSectionMap.ContainsKey(prop.Key))
                        continue;

                    var configurationSectionTypeInfo = configurationSectionMap[prop.Key];
                    var configurationSectionType = configurationSectionTypeInfo.AsType();
                    var configurationSection = (ConfigurationSection)prop.Value.ToObject(configurationSectionType);

                    configurationSections[configurationSectionType] = configurationSection;
                }
            }

            var configurationService = new ConfigurationService(configurationSections.Values);
        }

        private (string name, TypeInfo typeInfo) BuildMapping(TypeInfo typeInfo)
        {
            var name = typeInfo.Name.ToLower();

            if (name.EndsWith("configuration"))
                name = name.Substring(0, name.Length - "configuration".Length);

            if (name.EndsWith("configurationsection"))
                name = name.Substring(0, name.Length - "configurationsection".Length);

            return (name, typeInfo);
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