using System.Linq;
using System.Reflection;
using Glyde.AspNetCore.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GlydeSdk.Sample.WebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var dependencyContext = DependencyContext.Load(Assembly.GetEntryAssembly());
            var ownAssemblies = dependencyContext.RuntimeLibraries
                .SelectMany(l => l.GetDefaultAssemblyNames(dependencyContext).Select(Assembly.Load))
                .ToList();

            // Add framework services.
            services.AddMvcCore()
                .AddJsonFormatters(settings =>
                {
                    settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    settings.Formatting = Formatting.Indented;
                    settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                })
                .BootstrapApi(ownAssemblies, Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            app.UseGlydeSdkDefaults();
        }
    }

}