using Domain.Entities;
using Dtos;
using NUnit.Framework;
using Persistance.Finders.BrandFinder;
using System;
using System.Collections.Generic;

namespace Persistance.UnitTests.BrandFinderTests
{
    [TestFixture]
    public class GetAll : TestData
    {
        [Test]
        public void When_DataIsCorrect()
        {
            // Arange
            dbContext.Add(new Source
            {
                Id = 1,
                Name = "cars.bg"
            });

            dbContext.Add(new Source
            {
                Id = 2,
                Name = "mobile.bg"
            });

            dbContext.SaveChanges();

            Brand renault = new Brand
            {
                Id = 100,
                Name = "Renault",
            };

            BrandKey renaultBrandKey = new BrandKey
            {
                SourceId = 1,
                BrandId = 100,
                Key = "renault_key"
            };

            renault.BrandKeys.Add(renaultBrandKey);

            Brand mercedes = new Brand
            {
                Id = 101,
                Name = "Mercedes",
            };

            BrandKey mercedesBrandKey = new BrandKey
            {
                SourceId = 1,
                BrandId = 101,
                Key = "mercedes_key"
            };

            mercedes.BrandKeys.Add(mercedesBrandKey);

            Brand bmw = new Brand
            {
                Id = 102,
                Name = "BMW",
            };

            BrandKey bmwBrandKeyCarsBg = new BrandKey
            {
                SourceId = 1,
                BrandId = 102,
                Key = "bmw_key_cars_bg"
            };

            BrandKey bmwBrandKeyMobileBg = new BrandKey
            {
                SourceId = 2,
                BrandId = 102,
                Key = "bmw_key_mobile_bg"
            };

            bmw.BrandKeys.Add(bmwBrandKeyCarsBg);
            bmw.BrandKeys.Add(bmwBrandKeyMobileBg);

            dbContext.Brands.Add(renault);
            dbContext.Brands.Add(mercedes);
            dbContext.Brands.Add(bmw);
            dbContext.SaveChanges();

            BrandFinder brandFinder = new BrandFinder(dbContext);

            // Act
            List<BrandSearchDto> brands = brandFinder.GetBrandSearchDtos(SourceEnum.CarsBg);

            // Assert
            Assert.That(brands, Has.Count.EqualTo(3));

            Assert.That(brands[0].Id, Is.EqualTo(100));
            Assert.That(brands[0].BrandKey, Is.EqualTo("renault_key"));

            Assert.That(brands[1].Id, Is.EqualTo(101));
            Assert.That(brands[1].BrandKey, Is.EqualTo("mercedes_key"));

            Assert.That(brands[2].Id, Is.EqualTo(102));
            Assert.That(brands[2].BrandKey, Is.EqualTo("bmw_key_cars_bg"));

        }

        [Test]
        public void When_BrandMissesKeyForSource()
        {
            // Arange
            dbContext.Add(new Source
            {
                Id = 1,
                Name = "cars.bg"
            });

            dbContext.Add(new Source
            {
                Id = 2,
                Name = "mobile.bg"
            });

            dbContext.SaveChanges();

            Brand renault = new Brand
            {
                Id = 100,
                Name = "Renault",
            };

            BrandKey renaultBrandKey = new BrandKey
            {
                SourceId = 1,
                BrandId = 100,
                Key = "renault_key"
            };

            renault.BrandKeys.Add(renaultBrandKey);

            Brand mercedes = new Brand
            {
                Id = 101,
                Name = "Mercedes",
            };

            BrandKey mercedesBrandKey = new BrandKey
            {
                SourceId = 1,
                BrandId = 101,
                Key = "mercedes_key"
            };

            mercedes.BrandKeys.Add(mercedesBrandKey);

            Brand bmw = new Brand
            {
                Id = 102,
                Name = "BMW",
            };

            BrandKey bmwBrandKeyMobileBg = new BrandKey
            {
                SourceId = 2,
                BrandId = 102,
                Key = "bmw_key_mobile_bg"
            };

            bmw.BrandKeys.Add(bmwBrandKeyMobileBg);

            dbContext.Brands.Add(renault);
            dbContext.Brands.Add(mercedes);
            dbContext.Brands.Add(bmw);
            dbContext.SaveChanges();

            BrandFinder brandFinder = new BrandFinder(dbContext);

            // Act
            Assert.Throws<InvalidOperationException>(() => brandFinder.GetBrandSearchDtos(SourceEnum.CarsBg));
        }
    }
}
