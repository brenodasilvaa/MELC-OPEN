using Microsoft.EntityFrameworkCore;
using MELC.Main.API.Models;
using MELC.Core.Data;
using MELC.Core.Commons.Enums;

namespace MELC.Main.API.Data.Repository
{
    public class DesenhoRepository : IDesenhoRepository
    {
        public readonly MelcContext _context;

        public DesenhoRepository(MelcContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Desenho>> GetAllAsync()
        {
            return await _context.Desenhos.ToListAsync();
        }

        public async Task<Desenho> GetByIdAsync(Guid id)
        {
            return _context.Desenhos
                .Include(x => x.DesenhoServicos)
                .ThenInclude(x => x.TipoServico)
                .Include(x => x.DesenhoServicos)
                .ThenInclude(x => x.CriadoPor)
                .Include(x => x.MateriaisDesenhos)
                .ThenInclude(x => x.Solido)
                .Include(x => x.MateriaisDesenhos)
                .ThenInclude(x => x.Material)
                .Include(x => x.MateriaisDesenhos)
                .ThenInclude(x => x.CriadoPor)
                .Include(x => x.PecasNormalizadas)
                .ThenInclude(x => x.CriadoPor)
                .Include(x => x.CriadoPor)
                .Include(x => x.Arquivos)
                .Include(x => x.Pedido)
                .Include(x => x.FretesDesenhos)
                .ThenInclude(x => x.CriadoPor)
                .First(x => x.Id == id);
        }

        public async Task<IEnumerable<Desenho>> GetByDesenhoStatusAsync(Status status)
        {
            return await _context.Desenhos.Where(x => x.Status == status).ToListAsync();
        }

        public async Task<IEnumerable<Desenho>> GetByCriadoPorIdAsync(Guid userId)
        {
            return await _context.Desenhos.Where(x => x.CriadoPorId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Desenho>> GetByPedidoIdAsync(Guid pedidoId)
        {
            return await _context.Desenhos
                .Include(x => x.MateriaisDesenhos)
                .ThenInclude(x => x.Material)
                .Include(x => x.DesenhoServicos)
                .ThenInclude(x => x.TipoServico)
                .Include(x => x.PecasNormalizadas)
                .Include(x => x.FretesDesenhos)
                .Where(x => x.PedidoId == pedidoId).ToListAsync();
        }

        public async Task<Guid> InsertAsync(Desenho entity)
        {
            var resultado = await _context.Desenhos.AddAsync(entity);

            await _context.SaveChangesAsync();

            return resultado.Entity.Id;
        }

        public async Task<bool> NumeroDesenhoExiste(int numeroDesenho)
        {
            return await _context.Desenhos.AnyAsync(x => x.NumeroDesenho == numeroDesenho);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await _context.Desenhos.FindAsync(id);

            _context.MateriaisDesenhos.RemoveRange(_context.MateriaisDesenhos.Where(x => x.DesenhoId == id));
            _context.PecasNormalizadas.RemoveRange(_context.PecasNormalizadas.Where(x => x.DesenhoId == id));
            _context.Servicos.RemoveRange(_context.Servicos.Where(x => x.DesenhoId == id));

            _context.Desenhos.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task InsereArquivosAsync(IEnumerable<ArquivoDesenho> arquivosDesenho)
        {
            await _context.ArquivosDesenhos.AddRangeAsync(arquivosDesenho);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Desenho entity)
        {
            _context.Desenhos.Update(entity);

            await _context.SaveChangesAsync();
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task UpdateAll(IEnumerable<Desenho> desenhos)
        {
            _context.Desenhos.UpdateRange(desenhos);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Desenho>> GetByPrioridadeAsync()
        {
            return await _context.Desenhos
                .Include(x => x.Pedido)
                .ThenInclude(x => x.Cliente)
                .Where(x => x.Prioridade != 0)
                .OrderByDescending(x => x.Prioridade)
                .ToListAsync();
        }
    }
}
