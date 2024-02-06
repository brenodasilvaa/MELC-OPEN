using Microsoft.EntityFrameworkCore;
using MELC.Core.Data;
using MELC.Main.API.Models;

namespace MELC.Main.API.Data
{
    public class MelcContext : DbContext, IUnitOfWork
    {
        public MelcContext(DbContextOptions<MelcContext> options)
            : base(options) { }

        public DbSet<Desenho> Desenhos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ArquivoDesenho> ArquivosDesenhos { get; set; }
        public DbSet<DesenhoServico> Servicos { get; set; }
        public DbSet<TipoServico> TiposServicos { get; set; }
        public DbSet<Material> Materiais { get; set; }
        public DbSet<MaterialDesenho> MateriaisDesenhos { get; set; }
        public DbSet<Solido> Solidos { get; set; }
        public DbSet<PecaNormalizada> PecasNormalizadas { get; set; }
        public DbSet<Faturamento> Faturamentos { get; set; }
        public DbSet<Percentuais> Percentuais{ get; set; }
        public DbSet<FreteDesenho> FretesDesenhos{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MelcContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
