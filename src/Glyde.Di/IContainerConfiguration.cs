using System;
using System.Collections.Generic;
using Glyde.Di.Builder;

namespace Glyde.Di
{
    public interface IContainerConfiguration
    {
        void AddRegistration<TContract>(Lifecycle lifecycle, IContractToImplementationRegistration<TContract> registration) where TContract : class;

        void AddCollectionRegistration<TContract>(IEnumerable<IContractToImplementationRegistration<TContract>> registrations) 
            where TContract : class;
    }
}