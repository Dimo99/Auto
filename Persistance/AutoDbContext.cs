using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistance
{
    public class AutoDbContext : DbContext
    {
        public AutoDbContext(DbContextOptions<AutoDbContext> options)
             : base(options)
        {
        }

        public DbSet<Source> Sources { get; set; }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandKey> BrandKeys { get; set; }

        public DbSet<Model> Models { get; set; }
        public DbSet<ModelKey> ModelKeys { get; set; }

        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AutoDbContext).Assembly);
        }
    }
}
