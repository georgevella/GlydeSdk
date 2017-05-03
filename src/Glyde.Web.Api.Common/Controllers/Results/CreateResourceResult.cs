namespace Glyde.Web.Api.Controllers.Results
{
    public class CreateResourceResult<TResourceId>
    {
        public CreateResourceResult(CreateResourceResultType type, TResourceId id = default(TResourceId))
        {
            Type = type;
            Id = id;
        }

        public CreateResourceResultType Type { get; }

        public TResourceId Id { get; }
    }

    public enum CreateResourceResultType
    {
        Failed,
        AlreadyExists,
        Success
    }
}