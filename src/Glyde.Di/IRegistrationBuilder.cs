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

        void Use(TContract instance);
    }
}