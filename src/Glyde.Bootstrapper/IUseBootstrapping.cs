namespace Glyde.Bootstrapper
{
    public interface IUseBootstrapping
    {
        IUseBootstrapping RegisterBootstrapperStage<TBootstrapperStage>() where TBootstrapperStage : class, IBootstrapperStage;
        IUseBootstrapping RegisterBootstrapperStage(IBootstrapperStage bootstrapperStage);
    }
}