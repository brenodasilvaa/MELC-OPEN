using Microsoft.EntityFrameworkCore;
using MELC.Main.API.Models;

namespace MELC.Main.API.Data.Repository
{
    public class PercentuaisRepository : IPercentuaisRepository
    {
        public readonly MelcContext _context;

        public PercentuaisRepository(MelcContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Percentuais>> GetAllAsync()
        {
            return await _context.Percentuais.ToListAsync();
        }

        public async Task<Percentuais> GetByIdAsync(Guid id)
        {
            return await _context.Percentuais
                .FirstAsync(x => x.Id == id);
        }

        public async Task<Guid> InsertAsync(Percentuais entity)
        {
            var resultado = await _context.Percentuais.AddAsync(entity);

            await _context.SaveChangesAsync();

            return resultado.Entity.Id;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await _context.Percentuais.FindAsync(id);

            _context.Percentuais.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Percentuais entity)
        {
            var entidade = _context.Percentuais.FirstOrDefault(x => x.Id == entity.Id);

            if (entidade is not null)
            {
                entidade.Lucro = entity.Lucro;
                entidade.Impostos = entity.Impostos;
            }
            else
                await _context.Percentuais.AddAsync(entity);

            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
