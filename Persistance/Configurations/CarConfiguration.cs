using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("tCar");

            builder
              .HasKey(c => c.Id)
              .HasName("Pk_Car");

            builder
                .Property(c => c.Id)
                .UseIdentityColumn(1, 1);

            builder
                .Property(c => c.BrandId)
                .HasColumnName("Fk_BrandId");

            builder
                .HasOne(c => c.Brand)
                .WithMany(b => b.Cars)
                .HasForeignKey(c => c.BrandId)
                .HasPrincipalKey(b => b.Id)
                .HasConstraintName("Fk_Car_Brand")
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(c => c.ModelId)
                .HasColumnName("Fk_ModelId");

            builder
                .HasOne(c => c.Model)
                .WithMany(m => m.Cars)
                .HasForeignKey(c => c.ModelId)
                .HasPrincipalKey(m => m.Id)
                .HasConstraintName("Fk_Car_Model")
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(c => c.SourceId)
                .HasColumnName("Fk_SourceId");

            builder
                .HasOne(c => c.Source)
                .WithMany(s => s.Cars)
                .HasForeignKey(c => c.SourceId)
                .HasPrincipalKey(s => s.Id)
                .HasConstraintName("Fk_Car_Source")
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasIndex(c => c.AdUrl)
                .IsUnique()
                .HasName("UnIndx_AdUrl");

            builder
                .HasOne(c => c.MainInfo)
                .WithOne()
                .HasForeignKey<CarMainInfo>(c => c.Id);

            builder
                .HasOne(c => c.Comfort)
                .WithOne()
                .HasForeignKey<CarComfort>(c => c.Id);

            builder
                .HasOne(c => c.Safety)
                .WithOne()
                .HasForeignKey<CarSafety>(c => c.Id);
        }
    }
}
