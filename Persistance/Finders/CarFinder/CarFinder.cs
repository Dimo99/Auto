using Domain.Entities;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Persistance.Finders.CarFinder
{
    public class CarFinder
    {
        private readonly AutoDbContext dbContext;

        public CarFinder(AutoDbContext autoDbContext)
        {
            this.dbContext = autoDbContext;
        }

        public IEnumerable<Car> FilterNonExistingCars(IEnumerable<Car> cars)
        {
            ImmutableHashSet<string> carsUrls = dbContext.Cars.Select(c => c.AdUrl).ToImmutableHashSet();
            return cars.Where(c => c != null && !carsUrls.Contains(c.AdUrl));
        }

        public List<string> GetAddUrls()
        {
            return dbContext.Cars.Select(c => c.AdUrl).ToList();
        }
    }
}
