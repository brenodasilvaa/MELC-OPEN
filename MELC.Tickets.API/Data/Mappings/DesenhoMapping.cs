using MELC.Main.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MELC.Main.API.Data.Mappings
{
    public class DesenhoMapping : IEntityTypeConfiguration<Desenho>
    {
        public void Configure(EntityTypeBuilder<Desenho> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnType("varchar(300)");

            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasColumnType("varchar(1500)");

            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasColumnType("varchar(300)");

            builder.HasMany(x => x.DesenhoServicos).WithOne(x => x.Desenho);
            builder.HasOne(x => x.CriadoPor).WithMany(x => x.Desenhos).HasForeignKey(x => x.CriadoPorId).OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Desenhos");
        }
    }
}
