namespace Glyde.Web.Api.Resources
{
    public abstract class Resource<TResourceId> : IResource
    {
        public TResourceId Id { get; set; }
    }
}