using Glyde.Configuration.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Glyde.Configuration.Loaders
{
    public class JsonConfigurationFileLoader : IConfigurationLoader
    {
        private readonly string _filename;

        public JsonConfigurationFileLoader() : this("config.json")
        {

        }

        public JsonConfigurationFileLoader(string filename)
        {
            _filename = filename;
        }

        /// <inheritdoc />
        public IEnumerable<ConfigurationSection> Load(ApplicationConfigurationModel applicationConfigurationModel)
        {
            var configurationSectionMap = applicationConfigurationModel.ConfigurationSectionTypes
                .Select(BuildMapping)
                .ToDictionary(x => x.name, x => x.typeInfo);

            var configurationSections = new Dictionary<Type, ConfigurationSection>();

            if (!File.Exists(_filename))
                return configurationSections.Values;

            using (var json = new StreamReader(File.OpenRead(_filename)))
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

            return configurationSections.Values;
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
    }
}