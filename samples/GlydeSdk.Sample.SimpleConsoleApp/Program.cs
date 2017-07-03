using System;
using Glyde.ApplicationSupport;
using Glyde.ApplicationSupport.Extensions;
using Glyde.Configuration.Loaders;
using Glyde.Di;
using Glyde.Di.SimpleInjector;
using GlydeSdk.Sample.SimpleConsoleApp.Services;

namespace GlydeSdk.Sample.SimpleConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new ApplicationBootstrapper()
                .ConfigureApplicationUsing<EmptyConfigurationLoader>()
                .UseApplicationServices()
                .UseDependencyInjection(new SimpleInjectorContainerConfigurationFactory())
                .CreateApplication();

            app.Start();

            var service = app.Container.GetService<IService>();

            Console.WriteLine(service.GetValue());
        }
    }
}