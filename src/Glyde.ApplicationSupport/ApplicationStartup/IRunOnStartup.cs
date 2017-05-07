using System.Threading.Tasks;

namespace Glyde.ApplicationSupport.ApplicationStartup
{
    public interface IRunOnStartup
    {
        Task<IStartupServiceResult> Run();
    }
}