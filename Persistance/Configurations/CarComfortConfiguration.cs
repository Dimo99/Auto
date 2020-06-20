using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations
{
    public class CarComfortConfiguration : IEntityTypeConfiguration<CarComfort>
    {
        public void Configure(EntityTypeBuilder<CarComfort> builder)
        {
            builder.ToTable("tCar");
        }
    }
}
