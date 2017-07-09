using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Glyde.AspNetCore.Startup
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseGlydeSdkDefaults(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                // start dependency injection scope
                var scope = AspNetCoreApplicationContext.Instance.Container.StartScope();

                // setup httpcontextaccessor
                var httpContextAccessor = AspNetCoreApplicationContext.Instance.Container
                    .GetService<IHttpContextAccessor>();

                httpContextAccessor.HttpContext = context;

                try
                {
                    await next();
                }
                finally
                {
                    scope.Dispose();
                }
            });

            // invoke startup services
            using (AspNetCoreApplicationContext.Instance.Container.StartScope())
            {
                AspNetCoreApplicationContext.Instance.Start();
            }
        }
    }
}