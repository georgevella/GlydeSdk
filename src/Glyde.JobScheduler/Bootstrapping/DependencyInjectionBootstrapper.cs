using System;
using Glyde.Configuration;
using Glyde.Di;
using Glyde.Di.Bootstrapping;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace Glyde.JobScheduler.Bootstrapping
{
    public class DependencyInjectionBootstrapper : IDependencyInjectionBootstrapper
    {
        /// <inheritdoc />
        public void RegisterServices(IContainerBuilder containerBuilder, IConfigurationService configurationService)
        {
            // setup quartz
            ISchedulerFactory sf = new StdSchedulerFactory();
            containerBuilder.For<ISchedulerFactory>().Use(sf);
            containerBuilder.For<IJobFactory>().Use<SimpleInjectorJobFactory>().AsSingleton();
            containerBuilder.For<IScheduler>().Use<SchedulerAbsraction>().AsSingleton();
        }
    }
}