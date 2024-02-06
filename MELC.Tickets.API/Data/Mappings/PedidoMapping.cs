using MELC.Main.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MELC.Pedidos.API.Data.Mappings
{
    public class PedidoMapping : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(x => x.Descricao)
                .IsRequired(false)
                .HasColumnType("varchar(300)");

            builder.HasMany(x => x.Desenhos).WithOne(x => x.Pedido).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.CriadoPor).WithMany(x => x.Pedidos);

            builder.ToTable("Pedidos");
        }
    }
}
