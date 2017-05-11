using Glyde.Configuration.Extensions;
using Glyde.Configuration.Models;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace Glyde.Configuration.UnitTests
{
    public class TypeExtensionsTests
    {
        [Fact]
        public void Test1()
        {
            var a = typeof(TestConfigurationSection).GetTypeInfo().IsConfigurationSection();

            a.Should().Be(true);
        }
    }

    public class TestConfigurationSection : ConfigurationSection
    {

    }
}
