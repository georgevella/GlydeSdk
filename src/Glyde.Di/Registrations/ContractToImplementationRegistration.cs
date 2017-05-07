using System;
using Glyde.Di.Builder;
using Glyde.Di.Exceptions;

namespace Glyde.Di.Registrations
{
    internal class ContractToImplementationRegistration<TContract> :
        BaseRegistration,
        IContractToImplementationRegistration<TContract>,
        IRegistrationBuilder<TContract>
        where TContract : class
    {
        public Type ImplementationType { get; private set; }

        public Type FactoryType { get; private set; }

        public Func<TContract> FactoryMethod { get; private set; }

        public TContract Instance { get; private set; }

        public ContractToImplementationRegistration(Type implementationType = null, Type factoryType = null, TContract instance = null)
        {
            if (implementationType != null && factoryType != null)
                throw new AmbigousContainerRegistrationException();

            ImplementationType = implementationType;
            FactoryType = factoryType;
            Instance = instance;
        }
        public ContractToImplementationRegistration(Func<TContract> factoryMethod)
        {
            FactoryMethod = factoryMethod;
        }

        public IRegistrationLifecycleBuilder Use<TImplementation>()
            where TImplementation : TContract
        {
            ImplementationType = typeof(TImplementation);
            return new RegistrationLifeCycleBuilder(this);
        }

        public IRegistrationLifecycleBuilder UseFactory<TFactory>() where TFactory : IServiceFactory<TContract>
        {
            FactoryType = typeof(TFactory);
            return new RegistrationLifeCycleBuilder(this);
        }

        public IRegistrationLifecycleBuilder Use(Func<TContract> factoryMethod)
        {
            FactoryMethod = factoryMethod;
            return new RegistrationLifeCycleBuilder(this);
        }

        public override void Apply(IContainerConfiguration containerConfiguration)
        {
            containerConfiguration.AddRegistration(Lifecycle, this);
        }

        public void Use(TContract instance)
        {
            Instance = instance;
        }
    }
}