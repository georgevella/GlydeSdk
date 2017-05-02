using System.Collections.Generic;
using System.Threading.Tasks;
using Glyde.Web.Api.Controllers.Results;
using Glyde.Web.Api.Resources;

namespace Glyde.Web.Api.Controllers
{
    public interface IApiController<TResource, TResourceId>
        where TResource : Resource<TResourceId>
    {
        Task<IEnumerable<TResource>> GetAll();
        Task<TResource> Get(TResourceId id);
        Task<bool> Update(TResourceId id, TResource resource);
        Task<CreateResourceResult<TResourceId>> Create(TResource resource);
        Task<bool> Delete(TResourceId id);
    }
}