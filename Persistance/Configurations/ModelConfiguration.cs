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
                .Property(m => m.ParentModelId)
                .HasColumnName("Sk_ModelId");

            builder
                .HasOne(m => m.ParentModel)
                .WithMany(m => m.SubModels)
                .HasForeignKey(m => m.ParentModelId)
                .HasPrincipalKey(m => m.Id)
                .HasConstraintName("Sk_SeriesModel")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
