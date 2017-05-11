using Glyde.ApplicationSupport;
using Glyde.AspNetCore.Bootstrapping;
using Glyde.Bootstrapper;
using Glyde.Configuration.Loaders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.AspNetCore;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Glyde.AspNetCore.Startup
{
    public abstract class CommonGlydeAspNetStartup
    {
        private readonly Container _container = new Container();
        private IGlydeApplication _app;

        protected CommonGlydeAspNetStartup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            _container.Options.DefaultScopedLifestyle = new AspNetRequestLifestyle();
        }

        public IConfigurationRoot Configuration { get; }

        protected void ConfigureGlydeServices(ApplicationPartManager applicationPartManager, IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var dependencyContext = DependencyContext.Load(Assembly.GetEntryAssembly());
            var ownAssemblies = dependencyContext.RuntimeLibraries
                .SelectMany(l => l.GetDefaultAssemblyNames(dependencyContext).Select(Assembly.Load))
                .ToList();
            _app = new ApplicationBootstrapper(ownAssemblies, _container)
                .Using(new AspNetCoreBootstrapperStage(applicationPartManager, services, _container))
                .ConfigureApplicationUsing<JsonConfigurationFileLoader>()
                .CreateApplication();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // register http context accessor from app DI
            _container.Register(() => app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());

            app.UseSimpleInjectorAspNetRequestScoping(_container);

            app.UseMvc();

            using (var scope = _container.BeginExecutionContextScope())
            {
                _app.Start();

            }
        }

        //        _container.Register(GetAspNetServiceProvider<UserManager<MyUser>>(app));
        //_container.Register(GetAspNetServiceProvider<RoleManager<IdentityRole>>(app));
        //_container.Register(GetAspNetServiceProvider<SignInManager<MyUser>>(app));
        //
        //        private static Func<T> GetAspNetServiceProvider<T>(IApplicationBuilder app)
        //        {
        //            var accessor = app.ApplicationServices.GetService<IHttpContextAccessor>();
        //            return () => accessor.HttpContext.RequestServices.GetRequiredService<T>();
        //        }
    }

    public class OwnHttpContextAccessor : IHttpContextAccessor
    {
        private AsyncLocal<HttpContext> _httpContextCurrent = new AsyncLocal<HttpContext>();

        public HttpContext HttpContext
        {
            get
            {
                if (_httpContextCurrent.Value == null)
                    return null;
                return this._httpContextCurrent.Value;
            }
            set
            {
                this._httpContextCurrent.Value = value;
            }
        }
    }
}