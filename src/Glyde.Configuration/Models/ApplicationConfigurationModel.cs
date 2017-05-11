using System;
using System.Collections.Generic;
using System.Reflection;
using Glyde.Configuration.Extensions;

namespace Glyde.Configuration.Models
{
    public class ApplicationConfigurationModel
    {
        private readonly List<TypeInfo> _configurationSectionTypes = new List<TypeInfo>();

        public ApplicationConfigurationModel()
        {

        }

        public ApplicationConfigurationModel(IEnumerable<TypeInfo> typeInfo)
        {
            _configurationSectionTypes.AddRange(typeInfo);
        }

        public IEnumerable<TypeInfo> ConfigurationSectionTypes => _configurationSectionTypes;

        public void AddConfigurationSectionType(TypeInfo type)
        {
            if (type.IsConfigurationSection())
                _configurationSectionTypes.Add(type);

            throw new InvalidOperationException();
        }
    }
}