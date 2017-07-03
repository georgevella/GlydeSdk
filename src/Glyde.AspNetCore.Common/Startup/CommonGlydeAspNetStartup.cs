using System.Collections.Generic;
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
using Glyde.ApplicationSupport.Extensions;
using Glyde.Di;
using Glyde.Di.SimpleInjector;

namespace Glyde.AspNetCore.Startup
{
    public abstract class CommonGlydeAspNetStartup
    {
        protected CommonGlydeAspNetStartup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        protected IEnumerable<Assembly> GetAssemblies()
        {
            var dependencyContext = DependencyContext.Load(Assembly.GetEntryAssembly());
            var ownAssemblies = dependencyContext.RuntimeLibraries
                .SelectMany(l => l.GetDefaultAssemblyNames(dependencyContext).Select(Assembly.Load))
                .ToList();

            return ownAssemblies;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.StartGlydeApplication();
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