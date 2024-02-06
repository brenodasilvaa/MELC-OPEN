using Microsoft.EntityFrameworkCore;
using MELC.Main.API.Models;
using MELC.Core.Data;
using MELC.Core.Commons.Enums;

namespace MELC.Main.API.Data.Repository
{
    public class MaterialDesenhoRepository : IMaterialDesenhoRepository
    {
        public readonly MelcContext _context;

        public MaterialDesenhoRepository(MelcContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MaterialDesenho>> GetAllAsync()
        {
            return await _context.MateriaisDesenhos.ToListAsync();
        }

        public async Task<MaterialDesenho> GetByIdAsync(Guid id)
        {
            return await _context.MateriaisDesenhos
                .Include(x => x.Solido)
                .Include(x => x.CriadoPor)
                .FirstAsync(x => x.Id == id);
        }

        public async Task<Guid> InsertAsync(MaterialDesenho entity)
        {
            var resultado = await _context.MateriaisDesenhos.AddAsync(entity);

            await _context.SaveChangesAsync();

            return resultado.Entity.Id;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await _context.MateriaisDesenhos.FindAsync(id);

            _context.MateriaisDesenhos.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Update(MaterialDesenho entity)
        {
            _context.MateriaisDesenhos.Update(entity);

            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
