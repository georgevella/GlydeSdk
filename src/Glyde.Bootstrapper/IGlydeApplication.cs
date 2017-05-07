using System.Threading.Tasks;

namespace Glyde.Bootstrapper
{
    public interface IGlydeApplication
    {
        Task<IApplicationStartupResult> Start();
    }
}