using MELC.Main.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MELC.Main.API.Data.Mappings
{
    public class ArquivoDesenhoMapping : IEntityTypeConfiguration<ArquivoDesenho>
    {
        public void Configure(EntityTypeBuilder<ArquivoDesenho> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            builder.Property(x => x.NomeArquivo)
                .IsRequired()
                .HasColumnType("varchar(300)");

            builder.Property(x => x.CaminhoArquivo)
                .IsRequired()
                .HasColumnType("varchar(600)");

            builder.Property(x => x.Extensao)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.HasOne(x => x.Desenho).WithMany(x => x.Arquivos).HasForeignKey(x => x.DesenhoId);

            builder.ToTable("ArquivosDesenho");
        }
    }
}
