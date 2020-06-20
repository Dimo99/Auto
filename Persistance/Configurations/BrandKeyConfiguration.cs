using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations
{
    public class BrandKeyConfiguration : IEntityTypeConfiguration<BrandKey>
    {
        public void Configure(EntityTypeBuilder<BrandKey> builder)
        {
            builder.ToTable("tBrandSource");

            builder
                .HasKey(bk => new { bk.BrandId, bk.SourceId })
                .HasName("Pk_BrandSource");

            builder
                .Property(bk => bk.SourceId)
                .HasColumnName("Fk_SourceId");


            builder
                .HasOne(bk => bk.Source)
                .WithMany(s => s.BrandKeys)
                .HasForeignKey(bk => bk.SourceId)
                .HasPrincipalKey(s => s.Id)
                .HasConstraintName("Fk_BrandKey_Source")
                .OnDelete(DeleteBehavior.NoAction);
            
            builder
                .Property(bk => bk.BrandId)
                .HasColumnName("Fk_BrandId");

            builder
                .HasOne(bk => bk.Brand)
                .WithMany(b => b.BrandKeys)
                .HasForeignKey(bk => bk.BrandId)
                .HasPrincipalKey(b => b.Id)
                .HasConstraintName("Fk_BrandKey_Brand")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
