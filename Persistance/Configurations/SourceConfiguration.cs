using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations
{
    public class SourceConfiguration : IEntityTypeConfiguration<Source>
    {
        public void Configure(EntityTypeBuilder<Source> builder)
        {
            builder.ToTable("tSource");

            builder
                .HasKey(s => s.Id)
                .HasName("Pk_Source");

            builder
                .Property(s => s.Id)
                .UseIdentityColumn(1, 1);
        }
    }
}
