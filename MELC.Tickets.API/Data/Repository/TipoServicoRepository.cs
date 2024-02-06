using Microsoft.EntityFrameworkCore;
using MELC.Main.API.Models;

namespace MELC.Main.API.Data.Repository
{
    public class TipoServicoRepository : ITipoServicoRepository
    {
        public readonly MelcContext _context;

        public TipoServicoRepository(MelcContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoServico>> GetAllAsync()
        {
            return await _context.TiposServicos.ToListAsync();
        }

        public async Task<TipoServico> GetByIdAsync(Guid id)
        {
            return await _context.TiposServicos
                .FirstAsync(x => x.Id == id);
        }

        public async Task<Guid> InsertAsync(TipoServico entity)
        {
            var resultado = await _context.TiposServicos.AddAsync(entity);

            await _context.SaveChangesAsync();

            return resultado.Entity.Id;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await _context.TiposServicos.FindAsync(id);

            _context.TiposServicos.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Update(TipoServico entity)
        {
            _context.TiposServicos.Update(entity);

            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
