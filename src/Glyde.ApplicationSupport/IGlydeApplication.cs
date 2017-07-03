using System.Threading.Tasks;
using Glyde.Di;

namespace Glyde.ApplicationSupport
{
    public interface IGlydeApplication
    {
        IContainer Container { get; }
        Task<IApplicationStartupResult> Start();
    }
}