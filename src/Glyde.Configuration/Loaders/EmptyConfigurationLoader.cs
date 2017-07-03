using System.Collections.Generic;
using System.Linq;
using Glyde.Configuration.Models;

namespace Glyde.Configuration.Loaders
{
    public class EmptyConfigurationLoader : IConfigurationLoader
    {
        public IEnumerable<ConfigurationSection> Load(ApplicationConfigurationModel applicationConfigurationModel)
        {
            return Enumerable.Empty<ConfigurationSection>();
        }
    }
}