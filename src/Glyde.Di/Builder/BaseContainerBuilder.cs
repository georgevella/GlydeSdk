using Glyde.Di.Registrations;

namespace Glyde.Di.Builder
{
    public class ContainerBuilder : IContainerBuilder
    {
        internal ContainerRegistrationCollection Registrations { get; private set; } = new ContainerRegistrationCollection();

        public IRegistrationBuilder<TContract> For<TContract>() where TContract : class
        {
            var registration = new ContractToImplementationRegistration<TContract>();
            Registrations.Add(registration);
            return registration;
        }

        public ICollectionRegistrationBuilder<TContract> ForCollection<TContract>() where TContract : class
        {
            var registration = new CollectionRegistration<TContract>();
            Registrations.Add(registration);
            return registration;
        }

        public void Apply(IContainerConfiguration containerConfiguration)
        {
            foreach (var reg in Registrations)
            {
                reg.Apply(containerConfiguration);
            }
        }
    }
}