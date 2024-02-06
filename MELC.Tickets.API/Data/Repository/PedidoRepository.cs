using Microsoft.EntityFrameworkCore;
using MELC.Main.API.Models;
using MELC.Core.Data;
using MELC.Core.Commons.Enums;

namespace MELC.Main.API.Data.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        public readonly MelcContext _context;

        public PedidoRepository(MelcContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pedido>> GetAllAsync()
        {
            return await _context.Pedidos.OrderByDescending(x => x.NumeroPedido).ToListAsync();
        }

        public async Task<Pedido> GetByIdAsync(Guid id)
        {
            return _context.Pedidos
                .Include(x => x.CriadoPor)
                .Include(x => x.Desenhos)
                .ThenInclude(x => x.DesenhoServicos)
                .ThenInclude(x => x.TipoServico)
                .Include(x => x.Desenhos)
                .ThenInclude(x => x.DesenhoServicos)
                .ThenInclude(x => x.CriadoPor)
                .Include(x => x.Desenhos)
                .ThenInclude(x => x.MateriaisDesenhos)
                .ThenInclude(x => x.Material)
                .Include(x => x.Desenhos)
                .ThenInclude(x => x.MateriaisDesenhos)
                .ThenInclude(x => x.CriadoPor)
                .Include(x => x.Desenhos)
                .ThenInclude(x => x.PecasNormalizadas)
                .ThenInclude(x => x.CriadoPor)
                .Include(x => x.Desenhos)
                .ThenInclude(x => x.FretesDesenhos)
                .ThenInclude(x => x.CriadoPor)
                .Include(x => x.Faturamentos.OrderByDescending(x => x.Created))
                .ThenInclude(x => x.CriadoPor)
                .First(x => x.Id == id);
        }

        public async Task<IEnumerable<Pedido>> GetByStatusAsync(Status status)
        {
            return await _context.Pedidos.Where(x => x.Status == status).ToListAsync();
        }

        public async Task<IEnumerable<Pedido>> GetByCriadoPorIdAsync(Guid userId)
        {
            return await _context.Pedidos.Where(x => x.CriadoPorId == userId).ToListAsync();
        }

        public async Task<Guid> InsertAsync(Pedido novoPedido)
        {
            var resultado = await _context.Pedidos.AddAsync(novoPedido);
            await _context.SaveChangesAsync();

            return resultado.Entity.Id;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<IEnumerable<Pedido>> GetByClienteIdAsync(Guid clienteId)
        {
            return await _context.Pedidos.Where(x => x.ClienteId == clienteId).ToListAsync();
        }

        public async Task<bool> NumeroPedidoExiste(int numeroPedido)
        {
            return await _context.Pedidos.AnyAsync(x => x.NumeroPedido == numeroPedido);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await _context.Pedidos.FindAsync(id);

            _context.MateriaisDesenhos.RemoveRange(_context.MateriaisDesenhos.Where(x => x.Desenho.PedidoId == id));
            _context.PecasNormalizadas.RemoveRange(_context.PecasNormalizadas.Where(x => x.Desenho.PedidoId == id));
            _context.Servicos.RemoveRange(_context.Servicos.Where(x => x.Desenho.PedidoId == id));
            _context.Faturamentos.RemoveRange(_context.Faturamentos.Where(x => x.PedidoId == id));
            _context.Desenhos.RemoveRange(_context.Desenhos.Where(x => x.PedidoId == id));

            _context.Pedidos.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Pedido entity)
        {
            _context.Pedidos.Update(entity);

            await _context.SaveChangesAsync();
        }
    }
}
