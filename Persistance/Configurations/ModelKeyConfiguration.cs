using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations
{
    public class ModelKeyConfiguration : IEntityTypeConfiguration<ModelKey>
    {
        public void Configure(EntityTypeBuilder<ModelKey> builder)
        {
            builder.ToTable("tModelKey");

            builder
                .HasKey(mk => new { mk.ModelId, mk.SourceId })
                .HasName("Pk_ModelKey");

            builder
                .Property(mk => mk.ModelId)
                .HasColumnName("Fk_ModelId");

            builder
                .HasOne(mk => mk.Model)
                .WithMany(m => m.ModelKeys)
                .HasForeignKey(mk => mk.ModelId)
                .HasPrincipalKey(m => m.Id)
                .HasConstraintName("Fk_ModelKey_Model")
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Property(mk => mk.SourceId)
                .HasColumnName("Fk_SourceId");

            builder
                .HasOne(mk => mk.Source)
                .WithMany(s => s.ModelKeys)
                .HasForeignKey(mk => mk.SourceId)
                .HasPrincipalKey(s => s.Id)
                .HasConstraintName("Fk_ModelKey_Source")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
