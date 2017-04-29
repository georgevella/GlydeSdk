using Glyde.Di.Builder;

namespace Glyde.Di
{
    public interface IRegistration
    {
        Lifecycle Lifecycle { get; }
        void Apply(IContainerConfiguration containerConfiguration);
    }
}