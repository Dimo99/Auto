using Quartz;
using Quartz.Spi;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace Scheduling
{
    public class SingletonJobFactory : IJobFactory
    {
        private readonly IServiceProvider serviceProvider;

        public SingletonJobFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job) { }
    }
}
