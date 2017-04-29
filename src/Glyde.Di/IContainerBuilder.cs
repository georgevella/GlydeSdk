namespace Glyde.Di
{
    public interface IContainerBuilder
    {
        /// <summary>
        ///     Starts setting up a container registration for a contract / interface.
        /// </summary>
        /// <typeparam name="TContract">Interface to register with the container.</typeparam>
        /// <returns>An instance of the registration builder to continue setting up the registration.</returns>
        IRegistrationBuilder<TContract> For<TContract>()
            where TContract : class;

        /// <summary>
        ///     Starts or resumes setting up a container registration for a contract / interface that resolves multiple
        ///     instances.
        /// </summary>
        /// <typeparam name="TContract">Interface to register with the container.</typeparam>
        /// <returns>An instance of the registration builder to continue setting up the registration.</returns>
        ICollectionRegistrationBuilder<TContract> ForCollection<TContract>()
            where TContract : class;
    }
}