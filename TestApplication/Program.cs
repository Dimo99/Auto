using DataColector;
using DataColector.CarsBg;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistance;
using Persistance.Finders.BrandFinder;
using Persistance.Finders.CarFinder;
using Persistance.Finders.ModelFinder;


namespace TestApplication
{
    class Program
    {
        static void Main()
        {
            IServiceCollection servicesCollection = new ServiceCollection();

            Configure(servicesCollection);

            ServiceProvider serviceProvider = servicesCollection.BuildServiceProvider();

            ExistingBrands existingBrands = serviceProvider.GetService<ExistingBrands>();
            ExistingModels existingModels = serviceProvider.GetService<ExistingModels>();
            ExistingCars existingCars = serviceProvider.GetService<ExistingCars>();
            CarFinder carFinder = serviceProvider.GetService<CarFinder>();
            BrandFinder brandFinder = serviceProvider.GetService<BrandFinder>();
            ModelFinder modelFinder = serviceProvider.GetService<ModelFinder>();

            existingBrands.Load(brandFinder.GetBrands());
            existingModels.Load(modelFinder.GetModels());
            existingCars.Load(carFinder.GetAddUrls());

            InitialCollection.SaveAllBrands(serviceProvider);
            InitialCollection.SaveAllModels(serviceProvider);
            InitialCollection.SaveAllCars(serviceProvider);
        }

        public static void Configure(IServiceCollection services)
        {
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
        }
    }
}
