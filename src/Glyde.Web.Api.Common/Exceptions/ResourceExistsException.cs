using System;

namespace Glyde.Web.Api.Exceptions
{
    public class ResourceExistsException : Exception
    {
        public ResourceExistsException(object id) : base($"A resource with id '{id}' already exists.")
        {

        }
    }
}