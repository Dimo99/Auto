using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace TestApplication
{
    class Program
    {
        static void Main()
        {
            DbContextOptionsBuilder<AutoDbContext> optionsBuilder = new DbContextOptionsBuilder<AutoDbContext>();

            optionsBuilder
                .UseSqlServer(@"Server=DESKTOP-8AUT35O\SQL19;Database=AutoDatabase;User Id=sa;Password=root;",
                options => options.EnableRetryOnFailure());

            using (var dbContext = new AutoDbContext(optionsBuilder.Options))
            {
                dbContext.Sources.Add(new Source
                {
                    Name = "123"
                });

                dbContext.SaveChanges();
            }
        }
    }
}
