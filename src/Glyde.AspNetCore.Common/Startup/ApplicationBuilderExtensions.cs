using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;

namespace Glyde.AspNetCore.Startup
{
    public static class ApplicationBuilderExtensions
    {
        public static void StartGlydeApplication(this IApplicationBuilder app)
        {
            AspNetCoreApplication.Container.Register(() => app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());

            app.UseSimpleInjectorAspNetRequestScoping(AspNetCoreApplication.Container);

            app.UseMvc();

            using (var scope = AspNetCoreApplication.Container.BeginExecutionContextScope())
            {
                AspNetCoreApplication.Instance.Start();
            }
        }
    }
}