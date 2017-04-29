using Glyde.Di.Builder;

namespace Glyde.Di.Registrations
{
    internal abstract class BaseRegistration : IRegistration
    {
        public Lifecycle Lifecycle { get; internal set; }
        public abstract void Apply(IContainerConfiguration containerConfiguration);
    }
}