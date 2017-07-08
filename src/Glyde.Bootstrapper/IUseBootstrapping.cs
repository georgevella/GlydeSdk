namespace Glyde.Bootstrapper
{
    public interface IUseBootstrapping
    {
        IUseBootstrapping RegisterBootstrappingService<TBootstrappingService>()
            where TBootstrappingService : class;

        IUseBootstrapping RegisterBootstrappingService<TBootstrappingService, TImplementation>()
            where TBootstrappingService : class
            where TImplementation : TBootstrappingService;

        IUseBootstrapping RegisterBootstrappingService<TBootstrappingService>(TBootstrappingService instance)
            where TBootstrappingService : class;

        IUseBootstrapping RegisterBootstrapperStage<TBootstrappingStage>() 
            where TBootstrappingStage : class, IBootstrappingStage;
        IUseBootstrapping RegisterBootstrapperStage(IBootstrappingStage bootstrappingStage);
    }
}