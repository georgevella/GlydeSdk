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
        public static void UseGlydeSdkDefaults(this IApplicationBuilder app)
        {
            // TODO remove this hard dependency on SimpleInjector
            var simpleInjectorContainer = AspNetCoreApplicationContext.Instance.Container.AsSimpleInjectorContainer();

            // add a registration for IHttpContextAccessor within application container
            simpleInjectorContainer.Register(() => app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());

            // setup DI scoping
            app.UseSimpleInjectorAspNetRequestScoping(simpleInjectorContainer);

            // invoke startup services
            using (var scope = simpleInjectorContainer.BeginExecutionContextScope())
            {
                AspNetCoreApplicationContext.Instance.Start();
            }
        }
    }
}