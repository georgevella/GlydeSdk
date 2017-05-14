using System;

namespace Glyde.Di
{
    public interface IRegistrationBuilder<in TContract>
        where TContract : class
    {
        /// <summary>
        ///     Adds <typeparamref name="TImplementation"/> to the registration of <typeparamref name="TContract"/>.
        /// </summary>
        /// <typeparam name="TImplementation"></typeparam>
        /// <returns>An instance of the registration builder to set up the registration's lifecycle.</returns>
        IRegistrationLifecycleBuilder Use<TImplementation>()
            where TImplementation : TContract;

        IRegistrationLifecycleBuilder UseFactory<TFactory>()
            where TFactory : IServiceFactory<TContract>;

        IRegistrationLifecycleBuilder Use(Func<TContract> factoryMethod);

        /// <summary>
        ///     Creates a registration for <typeparamref name="TContract"/> that always returns the same instanced initialized externally.
        /// </summary>
        /// <param name="instance">The instance to use for all services depending on <typeparamref name="TContract"/>.</param>
        void Use(TContract instance);
    }
}