using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations
{
    public class CarSafetyConfiguration : IEntityTypeConfiguration<CarSafety>
    {
        public void Configure(EntityTypeBuilder<CarSafety> builder)
        {
            builder.ToTable("tCar");
        }
    }
}
