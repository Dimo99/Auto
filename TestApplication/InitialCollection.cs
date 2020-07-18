using DataColector;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Persistance;
using Persistance.Finders.BrandFinder;
using Persistance.Finders.ModelFinder;
using System;
using System.Collections.Generic;
using Common;
using System.Threading;
using Dtos;

namespace TestApplication
{
    public static class InitialCollection
    {
        public static void SaveAllBrands(IServiceProvider serviceProvider)
        {
            SaveBrandsForSource(serviceProvider, SourceEnum.CarsBg);
        }

        public static void SaveAllModels(IServiceProvider serviceProvider)
        {
            SaveAllModelsForSource(serviceProvider, SourceEnum.CarsBg);
        }

        public static void SaveAllCars(IServiceProvider serviceProvider)
        {
            SaveAllCarsForSource(serviceProvider, SourceEnum.CarsBg);
        }

        private static void SaveBrandsForSource(IServiceProvider serviceProvider, SourceEnum sourceEnum)
        {
            AutoDbContext dbContext = serviceProvider.GetService<AutoDbContext>();

            IEnumerable<Brand> brands = serviceProvider.GetDataCollector(sourceEnum).GetBrands();

            foreach (Brand brand in brands)
            {
                dbContext.Brands.Add(brand);
            }

            dbContext.SaveChanges();
        }

        private static void SaveAllModelsForSource(IServiceProvider serviceProvider, SourceEnum source)
        {
            List<BrandSearchDto> brandsSearch = serviceProvider.GetService<BrandFinder>().GetAll(source);
            AutoDbContext dbContext = serviceProvider.GetService<AutoDbContext>();

            foreach (BrandSearchDto brandSearch in brandsSearch)
            {
                IEnumerable<Model> models = serviceProvider.GetDataCollector(source).GetModels(brandSearch);

                foreach (Model model in models)
                {
                    dbContext.Models.Add(model);
                }

                dbContext.SaveChanges();
            }
        }

        private static void SaveAllCarsForSource(IServiceProvider serviceProvider, SourceEnum source)
        {
            int threads = 30;

            List<ModelSearchDto> list = serviceProvider.GetService<ModelFinder>().GetAll(source);
            IList<ModelSearchDto>[] bins = list.ToBins(threads);
            Thread[] thread = new Thread[threads];
            IServiceScope[] scopes = new IServiceScope[threads];

            try
            {
                for (int i = 0; i < bins.Length; i++)
                {
                    scopes[i] = serviceProvider.CreateScope();
                    IDataCollector dataCollector = scopes[i].ServiceProvider.GetDataCollector(source);
                    IServiceProvider scopedServiceProvider = scopes[i].ServiceProvider;
                    IList<ModelSearchDto> bin = bins[i];
                    thread[i] = new Thread(() => SaveCars(dataCollector, bin, scopedServiceProvider));
                    thread[i].Start();
                }

                for (int i = 0; i < thread.Length; i++)
                {
                    thread[i].Join();
                }
            }
            finally
            {
                for (int i = 0; i < scopes.Length; i++)
                {
                    scopes[i].Dispose();
                }
            }
        }

        private static void SaveCars(IDataCollector dataCollector, IList<ModelSearchDto> bin, IServiceProvider serviceProvider)
        {
            AutoDbContext dbContext = serviceProvider.GetService<AutoDbContext>();

            foreach (ModelSearchDto model in bin)
            {
                IEnumerable<Car> cars = dataCollector.GetAllCars(model);

                foreach (Car car in cars)
                {
                    dbContext.Cars.Add(car);
                }

                dbContext.SaveChanges();
            }
        }
    }
}
