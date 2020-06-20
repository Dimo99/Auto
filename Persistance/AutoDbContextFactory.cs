using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Resources;
using System;
using System.IO;

namespace Persistance
{
    public class AutoDbContextFactory : IDesignTimeDbContextFactory<AutoDbContext>
    {
        private const string MainApplicationFolder = "TestApplication";

        private const string ConnectionStringName = "AutoDatabase";

        public AutoDbContext CreateDbContext(string[] args)
        {
            return CreateDbContext();
        }

        public AutoDbContext CreateDbContext()
        {
            string basePath =
                Directory.GetCurrentDirectory() +
                string.Format("{0}..{0}{1}", Path.DirectorySeparatorChar, MainApplicationFolder);

            return Create(basePath);
        }

        private AutoDbContext Create(string basePath)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString(ConnectionStringName);

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException(string.Format(AutoResourceFile.ConnectionStringIsNullOrEmpty, ConnectionStringName), nameof(connectionString));
            }

            Console.WriteLine(string.Format(AutoResourceFile.ConnectionStringCreate, connectionString));

            var optionsBuilder = new DbContextOptionsBuilder<AutoDbContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return new AutoDbContext(optionsBuilder.Options);
        }
    }
}
