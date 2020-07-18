using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("tBrand");

            builder
                .HasKey(b => b.Id)
                .HasName("Pk_Brand");

            builder
                .Property(b => b.Id)
                .UseIdentityColumn(1, 1);

            builder
                .HasIndex(b => b.Name)
                .IsUnique()
                .HasName("Uq_Indx_Brand_Name");
        }
    }
}
