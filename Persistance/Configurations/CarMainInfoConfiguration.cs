using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations
{
    public class CarMainInfoConfiguration : IEntityTypeConfiguration<CarMainInfo>
    {
        public void Configure(EntityTypeBuilder<CarMainInfo> builder)
        {
            builder.ToTable("tCar");

            builder
                .Property(c => c.Engine)
                .HasConversion<string>();

            builder
                .Property(c => c.Transmision)
                .HasConversion<string>();
        }
    }
}
