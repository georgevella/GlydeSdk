namespace Glyde.Di
{
    public interface IRegistrationLifecycleBuilder
    {
        void AsSingleton();
        void AsTransient();
        void AsScoped();
    }
}