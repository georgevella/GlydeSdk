using System.Collections.Generic;
using Glyde.Di.Builder;

namespace Glyde.Di.Registrations
{
    internal class RegistrationLifeCycleBuilder : IRegistrationLifecycleBuilder
    {
        private readonly List<BaseRegistration> _registerations = new List<BaseRegistration>();   

        public RegistrationLifeCycleBuilder(BaseRegistration registeration)
        {
            _registerations.Add(registeration);
        }

        public RegistrationLifeCycleBuilder(IEnumerable<BaseRegistration> registrations)
        {
             _registerations.AddRange(registrations);
        }

        public void AsSingleton()
        {
            _registerations.ForEach(x => x.Lifecycle = Lifecycle.Singleton);
        }

        public void AsTransient()
        {
            _registerations.ForEach(x => x.Lifecycle = Lifecycle.Transient);
        }

        public void AsScoped()
        {
            _registerations.ForEach(x => x.Lifecycle = Lifecycle.Scoped);
        }
    }
}