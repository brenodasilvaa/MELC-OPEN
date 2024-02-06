using Microsoft.EntityFrameworkCore;
using MELC.Main.API.Models;

namespace MELC.Main.API.Data.Repository
{
    public class ArquivoDesenhoRepository : IArquivoDesenhoRepository
    {
        public readonly MelcContext _context;

        public ArquivoDesenhoRepository(MelcContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ArquivoDesenho>> GetAllAsync()
        {
            return await _context.ArquivosDesenhos.ToListAsync();
        }

        public async Task<ArquivoDesenho> GetByIdAsync(Guid id)
        {
            return await _context.ArquivosDesenhos
                .FirstAsync(x => x.Id == id);
        }

        public async Task<Guid> InsertAsync(ArquivoDesenho entity)
        {
            var resultado = await _context.ArquivosDesenhos.AddAsync(entity);

            await _context.SaveChangesAsync();

            return resultado.Entity.Id;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await _context.ArquivosDesenhos.FindAsync(id);

            _context.ArquivosDesenhos.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Update(ArquivoDesenho entity)
        {
            _context.ArquivosDesenhos.Update(entity);

            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
