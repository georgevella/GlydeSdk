using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Glyde.Web.Api.Controllers.Results;
using Glyde.Web.Api.Resources;

namespace Glyde.Web.Api.Controllers
{
    public abstract class ApiController<TResource, TResourceId> : IApiController<TResource, TResourceId>
        where TResource : Resource<TResourceId>
    {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public virtual async Task<IEnumerable<TResource>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public virtual async Task<TResource> Get(TResourceId id)
        {
            throw new System.NotImplementedException();
        }

        public virtual async Task<bool> Update(TResourceId id, TResource resource)
        {
            throw new System.NotImplementedException();
        }


        public virtual async Task<CreateResourceResult<TResourceId>> Create(TResource resource)
        {
            throw new System.NotImplementedException();
        }

        public virtual async Task<bool> Delete(TResourceId id)
        {
            throw new System.NotImplementedException();
        }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        protected CreateResourceResult<TResourceId> NotCreated()
        {
            return new CreateResourceResult<TResourceId>(CreateResourceResultType.Failed);
        }

        protected CreateResourceResult<TResourceId> AlreadyExists()
        {
            return new CreateResourceResult<TResourceId>(CreateResourceResultType.AlreadyExists);
        }

        protected CreateResourceResult<TResourceId> Created(TResourceId id)
        {
            return new CreateResourceResult<TResourceId>(CreateResourceResultType.Success, id);
        }

    }

}