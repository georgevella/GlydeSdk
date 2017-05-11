using System.Collections.Generic;
using Glyde.Configuration.Models;

namespace Glyde.Configuration
{
    public interface IConfigurationLoader
    {
        IEnumerable<ConfigurationSection> Load(ApplicationConfigurationModel applicationConfigurationModel);
    }
}