using MELC.Main.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MELC.Pedidos.API.Data.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(x => x.Cnpj)
                .IsRequired(false)
                .IsUnicode(true)
                .HasMaxLength(18);

            builder.HasMany(x => x.Pedidos).WithOne(x => x.Cliente).HasForeignKey(x => x.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Endereco);

            builder.ToTable("Clientes");
        }
    }
}
