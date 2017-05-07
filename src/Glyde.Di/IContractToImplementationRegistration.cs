using System;

namespace Glyde.Di
{
    public interface IContractToImplementationRegistration<out TContract> : IRegistration
    {
        Type ImplementationType { get; }
        Type FactoryType { get; }
        Func<TContract> FactoryMethod { get; }

        TContract Instance { get; }
    }
}