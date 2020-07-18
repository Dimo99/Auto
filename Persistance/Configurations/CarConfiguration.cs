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
                .Property(c => c.ComfortInfo)
                .HasConversion<int>();

            builder
                .Property(c => c.SafetyInfo)
                .HasConversion<int>();

            builder
                .Property(c => c.OtherInfo)
                .HasConversion<int>();
        }
    }
}
