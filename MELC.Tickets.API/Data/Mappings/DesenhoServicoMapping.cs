using MELC.Main.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MELC.Main.API.Data.Mappings
{
    public class DesenhoServicoMapping : IEntityTypeConfiguration<DesenhoServico>
    {
        public void Configure(EntityTypeBuilder<DesenhoServico> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            builder.HasOne(x => x.CriadoPor).WithMany(x => x.DesenhoServicos)
                .HasForeignKey(x => x.CriadoPorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Desenho).WithMany(x => x.DesenhoServicos);

            builder.ToTable("DesenhoServicos");
        }
    }
}
