using Microsoft.EntityFrameworkCore;
using MELC.Main.API.Models;

namespace MELC.Main.API.Data.Repository
{
    public class FaturamentoRepository : IFaturamentoRepository
    {
        public readonly MelcContext _context;

        public FaturamentoRepository(MelcContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Faturamento>> GetAllAsync()
        {
            return await _context.Faturamentos.ToListAsync();
        }

        public async Task<Faturamento> GetByIdAsync(Guid id)
        {
            return await _context.Faturamentos
                .FirstAsync(x => x.Id == id);
        }

        public async Task<Guid> InsertAsync(Faturamento entity)
        {
            var resultado = await _context.Faturamentos.AddAsync(entity);

            await _context.SaveChangesAsync();

            return resultado.Entity.Id;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await _context.Faturamentos.FindAsync(id);

            _context.Faturamentos.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Faturamento entity)
        {
            _context.Faturamentos.Update(entity);

            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
