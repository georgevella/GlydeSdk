using Glyde.Di.SimpleInjector;
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
            var simpleInjectorContainer = AspNetCoreApplicationContext.Instance.Container.AsSimpleInjectorContainer();

            simpleInjectorContainer.Register(() => app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());

            app.UseSimpleInjectorAspNetRequestScoping(simpleInjectorContainer);

            app.UseMvc();

            using (var scope = simpleInjectorContainer.BeginExecutionContextScope())
            {
                AspNetCoreApplicationContext.Instance.Start();
            }
        }
    }
}