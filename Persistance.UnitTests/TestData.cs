using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace Persistance.UnitTests
{
    public class TestData
    {
        protected AutoDbContext dbContext;

        [SetUp]
        public void Setup()
        {

            string dbName = Guid.NewGuid().ToString();
            DbContextOptions<AutoDbContext> options = new DbContextOptionsBuilder<AutoDbContext>()
                            .UseInMemoryDatabase(databaseName: dbName).Options;
            dbContext = new AutoDbContext(options);
        }

        [TearDown]
        public void Cleanup()
        {
            dbContext.Dispose();
        }
    }
}
