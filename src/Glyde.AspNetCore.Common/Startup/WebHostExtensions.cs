using Microsoft.AspNetCore.Hosting;

namespace Glyde.AspNetCore.Startup
{
    public static class WebHostExtensions
    {
        /// <summary>
        ///     Registers <see cref="GlydeAspNetMvcStartup"/> startup class with the web host builder.
        /// </summary>
        /// <param name="webHostBuilder">Instance of the webhost builder to configure.</param>
        /// <returns>The webhost builder instance to continue chaining more components.</returns>
        public static IWebHostBuilder UseGlydeBootstrappingForMvc(this IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.UseStartup<GlydeAspNetMvcStartup>();
            return webHostBuilder;
        }

        /// <summary>
        ///     Registers <see cref="GlydeAspNetApiStartup"/> startup class with the web host builder.
        /// </summary>
        /// <param name="webHostBuilder">Instance of the webhost builder to configure.</param>
        /// <returns>The webhost builder instance to continue chaining more components.</returns>
        public static IWebHostBuilder UseGlydeBootstrappingForApi(this IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.UseStartup<GlydeAspNetApiStartup>();
            return webHostBuilder;
        }
    }
}
