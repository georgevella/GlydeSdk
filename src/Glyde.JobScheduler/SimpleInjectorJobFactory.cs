using System;
using Quartz;
using Quartz.Spi;

namespace Glyde.JobScheduler
{
    public class SimpleInjectorJobFactory : IJobFactory
    {
        private readonly IServiceProvider _container;

        public SimpleInjectorJobFactory(IServiceProvider container)
        {
            _container = container;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return (IJob)_container.GetService(bundle.JobDetail.JobType);
        }

        public void ReturnJob(IJob job)
        {

        }
    }
}