using MELC.Main.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MELC.Main.API.Data.Mappings
{
    public class TipoServicoMapping : IEntityTypeConfiguration<TipoServico>
    {
        public void Configure(EntityTypeBuilder<TipoServico> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            builder.HasMany(x => x.DesenhoServicos).WithOne(x => x.TipoServico)
                .HasForeignKey(x => x.TipoServicoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("TiposServicos");
        }
    }
}
