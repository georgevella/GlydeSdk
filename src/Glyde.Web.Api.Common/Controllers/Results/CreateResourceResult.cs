namespace Glyde.Web.Api.Controllers.Results
{
    public class CreateResourceResult<TResourceId>
    {
        public CreateResourceResult(bool successful, TResourceId id = default (TResourceId))
        {
            Successful = successful;
            Id = id;
        }

        public bool Successful { get; }

        public TResourceId Id { get; }
    }
}