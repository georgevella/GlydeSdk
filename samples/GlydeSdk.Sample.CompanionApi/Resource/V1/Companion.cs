using Glyde.Web.Api.Resources;

namespace GlydeSdk.Sample.CompanionApi.Resource.V1
{
    [Resource("companion", Version = 1)]
    public class Companion : Resource<string>
    {
        public string Name { get; }

        public Companion()
        {
            
        }

        public Companion(string name)
        {
            Name = name;
        }
    }
}