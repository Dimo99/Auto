using Domain.Entities;
using Dtos;
using System.Collections.Generic;

namespace DataColector
{
    public interface IDataCollector
    {
        IEnumerable<Car> GetAllCars(ModelSearchDto model);
        IEnumerable<Brand> GetBrands();
        IEnumerable<Model> GetModels(BrandSearchDto brand);
    }
}
