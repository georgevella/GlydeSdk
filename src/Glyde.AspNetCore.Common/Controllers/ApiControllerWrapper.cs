using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Glyde.AspNetCore.Versioning;
using Glyde.Web.Api.Controllers;
using Glyde.Web.Api.Resources;
using Glyde.Web.Api.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Glyde.AspNetCore.Controllers
{
    [IgnoreVersioningConvention]
    public class ApiControllerWrapper<TApiController, TResource, TResourceId> : ControllerBase
        where TApiController : IApiController<TResource, TResourceId>
        where TResource : Resource<TResourceId>
    {
        private readonly IApiController<TResource, TResourceId> _apiController;
        private readonly IEnumerable<MethodInfo> _methods;

        public ApiControllerWrapper(TApiController apiController)
        {
            _apiController = apiController;

            var typeInfo = _apiController.GetType().GetTypeInfo();
            _methods = typeInfo.DeclaredMethods;
        }

        private bool CanInvokeAction(Expression<Action> actionExpression)
        {
            var method = ((MethodCallExpression) actionExpression.Body).Method;
            return _methods.Contains(method);
        }

        [HttpGet]
        [Route("")]
        public virtual async Task<IActionResult> GetAll()
        {            
            if (CanInvokeAction(() => _apiController.GetAll()))
                return StatusCode(StatusCodes.Status405MethodNotAllowed);

            return new JsonResult(await _apiController.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public virtual async Task<IActionResult> Get(TResourceId id)
        {
            if (CanInvokeAction(() => _apiController.Get(id)))
                return StatusCode(StatusCodes.Status405MethodNotAllowed);

            try
            {
                var item = await _apiController.Get(id);

                if (item != null)
                {
                    return new JsonResult(item);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }            

            return NotFound();
        }

        [HttpPut]
        [Route("{id}")]
        public virtual async Task<IActionResult> Update(TResourceId id, [FromBody]TResource resource)
        {
            if (CanInvokeAction(() => _apiController.Update(id, resource)))
                return StatusCode(StatusCodes.Status405MethodNotAllowed);

            try
            {
                var result = await _apiController.Update(id, resource);

                return result ? NoContent() : new StatusCodeResult(StatusCodes.Status304NotModified);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPost]
        [Route("")]
        public virtual async Task<IActionResult> Create([FromBody]TResource resource)
        {
            if (CanInvokeAction(() => _apiController.Create(resource)))
                return StatusCode(StatusCodes.Status405MethodNotAllowed);

            try
            {
                var result = await _apiController.Create(resource);
                if (result.Successful)
                {
                    // TODO: build get uri from id returned above
                    return Created(string.Empty, resource);
                }

                return StatusCode(StatusCodes.Status304NotModified);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual async Task<IActionResult> Delete(TResourceId id)
        {
            if (CanInvokeAction(() => _apiController.Delete(id)))
                return StatusCode(StatusCodes.Status405MethodNotAllowed);

            try
            {
                return await _apiController.Delete(id) ? (IActionResult)NoContent() : NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }
    }
}