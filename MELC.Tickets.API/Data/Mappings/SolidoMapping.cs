using MELC.Main.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MELC.Main.API.Data.Mappings
{
    public class SolidoMapping : IEntityTypeConfiguration<Solido>
    {
        public void Configure(EntityTypeBuilder<Solido> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            builder.ToTable("Solidos");
        }
    }
}
