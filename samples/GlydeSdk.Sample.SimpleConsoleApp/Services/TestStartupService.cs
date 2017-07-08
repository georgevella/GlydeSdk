using System;
using System.Threading.Tasks;
using Glyde.ApplicationSupport.ApplicationStartup;

namespace GlydeSdk.Sample.SimpleConsoleApp.Services
{
    public class TestStartupService : IRunOnStartup
    {
        public async Task<IStartupServiceResult> Run()
        {
            Console.WriteLine("TestStartupService::Run()");

            return await Task.FromResult(new SuccessfulStartupServiceResult());
        }
    }
}