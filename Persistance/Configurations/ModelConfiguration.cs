using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations
{
    public class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.ToTable("tModel");

            builder
                .HasKey(m => m.Id)
                .HasName("Pk_Model");

            builder
                .Property(m => m.Id)
                .UseIdentityColumn(1, 1);

            builder
                .Property(m => m.BrandId)
                .HasColumnName("Fk_BrandId");

            builder
                .HasOne(m => m.Brand)
                .WithMany(b => b.Models)
                .HasForeignKey(m => m.BrandId)
                .HasPrincipalKey(b => b.Id)
                .HasConstraintName("Fk_Model_Brand")
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(m => m.ModelId)
                .HasColumnName("Sk_ModelId");

            builder
                .HasOne(m => m.SeriesModel)
                .WithMany(m => m.SubModels)
                .HasForeignKey(m => m.ModelId)
                .HasPrincipalKey(m => m.Id)
                .HasConstraintName("Sk_SeriesModel")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
