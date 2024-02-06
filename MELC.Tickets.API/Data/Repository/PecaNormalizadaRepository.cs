using Microsoft.EntityFrameworkCore;
using MELC.Main.API.Models;

namespace MELC.Main.API.Data.Repository
{
    public class PecaNormalizadaRepository : IPecaNormalizadaRepository
    {
        public readonly MelcContext _context;

        public PecaNormalizadaRepository(MelcContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PecaNormalizada>> GetAllAsync()
        {
            return await _context.PecasNormalizadas.ToListAsync();
        }

        public async Task<PecaNormalizada> GetByIdAsync(Guid id)
        {
            return await _context.PecasNormalizadas
                .Include(x => x.Desenho)
                .Include(x => x.CriadoPor)
                .FirstAsync(x => x.Id == id);
        }

        public async Task<Guid> InsertAsync(PecaNormalizada entity)
        {
            var resultado = await _context.PecasNormalizadas.AddAsync(entity);

            await _context.SaveChangesAsync();

            return resultado.Entity.Id;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await _context.PecasNormalizadas.FindAsync(id);

            _context.PecasNormalizadas.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Update(PecaNormalizada entity)
        {
            _context.PecasNormalizadas.Update(entity);

            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
