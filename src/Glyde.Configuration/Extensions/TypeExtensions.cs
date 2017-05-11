using System;
using System.Reflection;
using Glyde.Configuration.Models;

namespace Glyde.Configuration.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsConfigurationSection(this TypeInfo typeinfo)
        {
            var type = typeinfo.AsType();

            if (typeinfo.IsAbstract)
                return false;

            if (type == typeof(object))
                return false;

            if (type == typeof(ConfigurationSection))
                return true;

            return IsConfigurationSection(typeinfo.BaseType.GetTypeInfo());
        }
    }
}