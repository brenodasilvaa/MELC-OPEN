using MELC.Main.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MELC.Main.API.Data.Mappings
{
    public class MaterialDesenhoMapping : IEntityTypeConfiguration<MaterialDesenho>
    {
        public void Configure(EntityTypeBuilder<MaterialDesenho> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");

            builder.HasOne(x => x.CriadoPor).WithMany(x => x.MaterialDesenho)
                .HasForeignKey(x => x.CriadoPorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Desenho).WithMany(x => x.MateriaisDesenhos)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Material).WithMany(x => x.MateriaisDesenhos)
                .HasForeignKey(x => x.MaterialId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Solido);

            builder.ToTable("MateriaisDesenhos");
        }
    }
}
