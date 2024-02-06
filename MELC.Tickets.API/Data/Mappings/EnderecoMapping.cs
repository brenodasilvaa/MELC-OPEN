using MELC.Main.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MELC.Pedidos.API.Data.Mappings
{
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Rua)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(x => x.Cidade)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(x => x.Numero)
                .IsRequired(false);

            builder.ToTable("Enderecos");
        }
    }
}
