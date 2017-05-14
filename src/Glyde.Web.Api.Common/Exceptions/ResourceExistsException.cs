using System;

namespace Glyde.Web.Api.Exceptions
{
    public class ResourceExistsException : Exception
    {
        public ResourceExistsException(object id) : base($"A resource with id '{id}' already exists.")
        {

        }
    }

    public class ResourceDoesNotExistException : Exception
    {
        public ResourceDoesNotExistException(object id) : base($"A resource with id '{id}' does not exist.")
        {

        }
    }


    public class UnsupportedOperationException : Exception
    {
        public UnsupportedOperationException(object id) : base($"The requested operation is not supported on the resource.")
        {
        }
    }
}