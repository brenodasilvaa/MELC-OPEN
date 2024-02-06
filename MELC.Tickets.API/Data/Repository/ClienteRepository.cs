using Microsoft.EntityFrameworkCore;
using MELC.Main.API.Models;
using MELC.Core.Data;

namespace MELC.Main.API.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        public readonly MelcContext _context;

        public ClienteRepository(MelcContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            return await _context.Clientes.OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<Cliente> GetByIdAsync(Guid id)
        {
            return await _context.Clientes.Include(x => x.Endereco).FirstAsync(x => x.Id == id);
        }

        public bool ClienteExiste(Cliente cliente)
        {
            return _context.Clientes.Any(x => x.Nome == cliente.Nome || x.Cnpj == cliente.Cnpj);
        }

        public async Task<Guid> InsertAsync(Cliente novoCliente)
        {
            var resultado = await _context.Clientes.AddAsync(novoCliente);

            await _context.SaveChangesAsync();

            return resultado.Entity.Id;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await _context.Clientes.FindAsync(id);

            _context.MateriaisDesenhos.RemoveRange(_context.MateriaisDesenhos.Where(x => x.Desenho.Pedido.ClienteId == id));
            _context.PecasNormalizadas.RemoveRange(_context.PecasNormalizadas.Where(x => x.Desenho.Pedido.ClienteId == id));
            _context.Servicos.RemoveRange(_context.Servicos.Where(x => x.Desenho.Pedido.ClienteId == id));
            _context.Faturamentos.RemoveRange(_context.Faturamentos.Where(x => x.Pedido.ClienteId == id));
            _context.Desenhos.RemoveRange(_context.Desenhos.Where(x => x.Pedido.ClienteId == id));
            _context.Pedidos.RemoveRange(_context.Pedidos.Where(x => x.ClienteId == id));

            _context.Clientes.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Cliente entity)
        {
            _context.Clientes.Update(entity);

            await _context.SaveChangesAsync();
        }
    }
}
