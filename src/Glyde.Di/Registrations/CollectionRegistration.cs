using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Glyde.Di.Builder;

namespace Glyde.Di.Registrations
{
    internal class CollectionRegistration<TContract> : 
        BaseRegistration,
        ICollectionRegistrationBuilder<TContract> 
        where TContract : class
    {
        public List<IContractToImplementationRegistration<TContract>> Registrations { get; private set; } = new List<IContractToImplementationRegistration<TContract>>();

        public IRegistrationLifecycleBuilder Use<TImplementation>() where TImplementation : TContract
        {
            var reg = new ContractToImplementationRegistration<TContract>(typeof(TImplementation));
            Registrations.Add(reg);

            return new RegistrationLifeCycleBuilder(reg);
        }

        public IRegistrationLifecycleBuilder UseAllInAssembly(Assembly assembly)
        {
            var registrations = assembly.DefinedTypes
                .Where(t => t.ImplementedInterfaces.Contains(typeof(TContract)))
                .Select(t => new ContractToImplementationRegistration<TContract>(t.AsType()))
                .ToList();

            return new RegistrationLifeCycleBuilder(registrations);

        }

        public IRegistrationLifecycleBuilder UseFactory<TFactory>() where TFactory : IServiceFactory<TContract>
        {
            var reg = new ContractToImplementationRegistration<TContract>(factoryType: typeof(TFactory));
            Registrations.Add(reg);

            return new RegistrationLifeCycleBuilder(reg);
        }

        public IRegistrationLifecycleBuilder Use(Func<TContract> factoryMethod)
        {
            var reg = new ContractToImplementationRegistration<TContract>(factoryMethod);
            Registrations.Add(reg);

            return new RegistrationLifeCycleBuilder(reg);
        }

        public override void Apply(IContainerConfiguration containerConfiguration)
        {
            containerConfiguration.AddCollectionRegistration<TContract>(Registrations);
        }

        public void Use(TContract instance)
        {
            var reg = new ContractToImplementationRegistration<TContract>(instance: instance);
            Registrations.Add(reg);
        }
    }
}