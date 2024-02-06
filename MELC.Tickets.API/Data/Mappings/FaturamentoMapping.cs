using MELC.Main.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MELC.Main.API.Data.Mappings
{
    public class FaturamentoMapping : IEntityTypeConfiguration<Faturamento>
    {
        public void Configure(EntityTypeBuilder<Faturamento> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnType("varchar(300)");

            builder.Property(x => x.NomeArquivo)
                .IsRequired()
                .HasColumnType("varchar(300)");

            builder.Property(x => x.CaminhoArquivo)
                .IsRequired()
                .HasColumnType("varchar(600)");

            builder.Property(x => x.Extensao)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(x => x.Pecas)
                .IsRequired()
                .HasColumnType("varchar(MAX)");

            builder.HasOne(x => x.CriadoPor).WithMany(x => x.Faturamentos).HasForeignKey(x => x.CriadoPorId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Pedido).WithMany(x => x.Faturamentos).HasForeignKey(x => x.PedidoId).OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Faturamentos");
        }
    }
}
