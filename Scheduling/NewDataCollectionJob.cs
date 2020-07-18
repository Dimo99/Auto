using Common;
using DataColector;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistance;
using Persistance.Migrations;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Scheduling
{
    public class NewDataCollectionJob : IJob
    {
        private IServiceProvider serviceProvider;
        private ILogger logger;

        public NewDataCollectionJob(IServiceProvider serviceProvider, ILogger logger)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                scope.ServiceProvider.GetService<CollectData>().SaveNewCars(scope.ServiceProvider);
            }

            return Task.CompletedTask;
        }
    }
}
