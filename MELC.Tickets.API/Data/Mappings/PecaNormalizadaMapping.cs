using MELC.Main.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MELC.Main.API.Data.Mappings
{
    public class PecaNormalizadaMapping : IEntityTypeConfiguration<PecaNormalizada>
    {
        public void Configure(EntityTypeBuilder<PecaNormalizada> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            builder.HasOne(x => x.CriadoPor).WithMany(x => x.PecasNormalizadas)
                .HasForeignKey(x => x.CriadoPorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Desenho).WithMany(x => x.PecasNormalizadas)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("PecasNormalizadas");
        }
    }
}
