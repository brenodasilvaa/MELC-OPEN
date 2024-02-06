using Microsoft.EntityFrameworkCore;
using MELC.Main.API.Models;

namespace MELC.Main.API.Data.Repository
{
    public class MaterialRepository : IMaterialRepository
    {
        public readonly MelcContext _context;

        public MaterialRepository(MelcContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Material>> GetAllAsync()
        {
            return await _context.Materiais.ToListAsync();
        }

        public async Task<Material> GetByIdAsync(Guid id)
        {
            return await _context.Materiais
                .FirstAsync(x => x.Id == id);
        }

        public async Task<Guid> InsertAsync(Material entity)
        {
            var resultado = await _context.Materiais.AddAsync(entity);

            await _context.SaveChangesAsync();

            return resultado.Entity.Id;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await _context.Materiais.FindAsync(id);

            _context.Materiais.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Update(Material entity)
        {
            _context.Materiais.Update(entity);

            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
