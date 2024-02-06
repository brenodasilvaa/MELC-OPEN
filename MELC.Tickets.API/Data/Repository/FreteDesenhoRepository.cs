using Microsoft.EntityFrameworkCore;
using MELC.Main.API.Models;
    
namespace MELC.Main.API.Data.Repository
{
    public class FreteDesenhoRepository : IFreteDesenhoRepository
    {
        public readonly MelcContext _context;

        public FreteDesenhoRepository(MelcContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FreteDesenho>> GetAllAsync()
        {
            return await _context.FretesDesenhos.ToListAsync();
        }

        public async Task<FreteDesenho> GetByIdAsync(Guid id)
        {
            return await _context.FretesDesenhos
                .Include(x => x.Desenho)
                .Include(x => x.CriadoPor)
                .FirstAsync(x => x.Id == id);
        }

        public async Task<Guid> InsertAsync(FreteDesenho entity)
        {
            var resultado = await _context.FretesDesenhos.AddAsync(entity);

            await _context.SaveChangesAsync();

            return resultado.Entity.Id;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await _context.FretesDesenhos.FindAsync(id);

            _context.FretesDesenhos.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Update(FreteDesenho entity)
        {
            _context.FretesDesenhos.Update(entity);

            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
