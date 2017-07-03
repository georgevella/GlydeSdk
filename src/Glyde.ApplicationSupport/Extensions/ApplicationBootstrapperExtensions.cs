using Glyde.ApplicationSupport.Bootstrapping;
using Glyde.Bootstrapper;

namespace Glyde.ApplicationSupport.Extensions
{
    public static class ApplicationBootstrapperExtensions
    {
        public static T UseApplicationServices<T>(
            this T applicationBootstrapper)
            where  T: IUseBootstrapping
        {
            applicationBootstrapper.RegisterBootstrapperStage<ApplicationServicesBootstrapperStage>();
            return applicationBootstrapper;            
        }
    }
}