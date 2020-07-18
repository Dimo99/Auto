﻿using DataColector;
using DataColector.CarsBg;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistance;
using Persistance.Finders.BrandFinder;
using Persistance.Finders.CarFinder;
using Persistance.Finders.ModelFinder;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Scheduling;
using System.Threading;
using System.Threading.Tasks;

namespace TestApplication
{
    class Program
    {
        static async Task Main()
        {
            ILogger logger = GetLogger();
            IServiceCollection serviceCollection = ConfigureServices(logger);
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            var quartz = serviceProvider.GetService<QuartzHostedService>();

            await quartz.StartAsync(new CancellationToken());

            Thread.Sleep(1000000000);
        }

        private static void LoadCars(ServiceProvider serviceProvider)
        {
            ExistingCars existingCars = serviceProvider.GetService<ExistingCars>();
            CarFinder carFinder = serviceProvider.GetService<CarFinder>();
            existingCars.Load(carFinder.GetAddUrls());
        }

        private static void LoadModels(ServiceProvider serviceProvider)
        {
            ExistingModels existingModels = serviceProvider.GetService<ExistingModels>();
            ModelFinder modelFinder = serviceProvider.GetService<ModelFinder>();
            existingModels.Load(modelFinder.GetModels());
        }

        private static void LoadBrands(ServiceProvider serviceProvider)
        {
            ExistingBrands existingBrands = serviceProvider.GetService<ExistingBrands>();
            BrandFinder brandFinder = serviceProvider.GetService<BrandFinder>();
            existingBrands.Load(brandFinder.GetBrands());
        }

        private static ILogger GetLogger()
        {
            ILoggerFactory loggerFactory = new LoggerFactory(new[] { new Log4NetProvider() });
            ILogger logger = loggerFactory.CreateLogger<Program>();
            return logger;
        }

        public static IServiceCollection ConfigureServices(ILogger logger)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddDbContext<AutoDbContext>(
                options =>
                options
                .UseSqlServer(@"Server=DESKTOP-8AUT35O\SQL19;Database=AutoDatabase;User Id=sa;Password=root;"));

            services.AddScoped<BrandFinder>();
            services.AddScoped<ModelFinder>();
            services.AddScoped<CarFinder>();
            services.AddScoped<CarsBgDataCollector>();
            services.AddScoped<DataCollectorFactory>();
            services.AddSingleton<ExistingBrands>();
            services.AddSingleton<ExistingModels>();
            services.AddSingleton<ExistingCars>();
            services.AddSingleton(logger);

            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddSingleton<NewDataCollectionJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(NewDataCollectionJob),
                cronExpression: "0 0/15 0 ? * * *")); // run every 15 minutes

            services.AddSingleton<QuartzHostedService>();

            return services;
        }
    }
}
