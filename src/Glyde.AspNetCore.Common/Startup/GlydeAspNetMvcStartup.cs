using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;

namespace Glyde.AspNetCore.Startup
{
    public class GlydeAspNetMvcStartup : CommonGlydeAspNetStartup
    {
        public GlydeAspNetMvcStartup(IHostingEnvironment env) : base(env)
        {

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().BootstrapMvc(GetAssemblies(), Configuration);
        }        
    }
}
