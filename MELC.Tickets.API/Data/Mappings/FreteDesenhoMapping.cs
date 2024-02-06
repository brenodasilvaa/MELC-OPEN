using MELC.Main.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MELC.Main.API.Data.Mappings
{
    public class FreteDesenhoMapping : IEntityTypeConfiguration<FreteDesenho>
    {
        public void Configure(EntityTypeBuilder<FreteDesenho> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            builder.HasOne(x => x.CriadoPor).WithMany(x => x.FretesDesenhos)
                .HasForeignKey(x => x.CriadoPorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Desenho).WithMany(x => x.FretesDesenhos)
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("FretesDesenhos");
        }
    }
}
