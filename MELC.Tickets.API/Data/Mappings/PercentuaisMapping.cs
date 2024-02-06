using MELC.Main.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MELC.Main.API.Data.Mappings
{
    public class PercentuaisMapping : IEntityTypeConfiguration<Percentuais>
    {
        public void Configure(EntityTypeBuilder<Percentuais> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Lucro).IsRequired(false);
            builder.Property(x => x.Impostos).IsRequired(false);

            builder.ToTable("Percentuais");
        }
    }
}
