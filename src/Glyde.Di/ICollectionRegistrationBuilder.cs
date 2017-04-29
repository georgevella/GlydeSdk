using System.Reflection;

namespace Glyde.Di
{
    public interface ICollectionRegistrationBuilder<in TContract> : IRegistrationBuilder<TContract>
        where TContract : class
    {
        /// <summary>
        ///     Finds all implementations of <typeparamref name="TContract"/> in the calling assembly.
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns>An instance of the registration builder to set up the registration's lifecycle.</returns>
        IRegistrationLifecycleBuilder UseAllInAssembly(Assembly assembly);
    }
}