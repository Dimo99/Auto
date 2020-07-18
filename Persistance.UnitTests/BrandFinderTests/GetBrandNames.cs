using Domain.Entities;
using NUnit.Framework;
using Persistance.Finders.BrandFinder;
using System.Collections.Generic;

namespace Persistance.UnitTests.BrandFinderTests
{
    [TestFixture]
    public class GetBrandNames : TestData
    {
        [Test]
        public void When()
        {
            // Arange
            dbContext.Add(new Brand
            {
                Name = "Renault",
                Id = 100
            });

            dbContext.Add(new Brand
            {
                Name = "Mercedes",
                Id = 101
            });

            dbContext.Add(new Brand
            {
                Name = "BMW",
                Id = 123
            });

            dbContext.SaveChanges();

            BrandFinder brandFinder = new BrandFinder(dbContext);

            // Act
            List<string> brandNames = brandFinder.GetBrandNames();

            // Assert
            Assert.That(brandNames, Has.Count.EqualTo(3));

            Assert.That(brandNames[0], Is.EqualTo("Renault"));
            Assert.That(brandNames[1], Is.EqualTo("Mercedes"));
            Assert.That(brandNames[2], Is.EqualTo("BMW"));
        }
    }
}
