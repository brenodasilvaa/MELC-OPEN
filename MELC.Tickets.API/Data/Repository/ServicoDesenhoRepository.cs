using Microsoft.EntityFrameworkCore;
using MELC.Main.API.Models;
using MELC.Core.Data;
using MELC.Core.Commons.Enums;

namespace MELC.Main.API.Data.Repository
{
    public class ServicoDesenhoRepository : IServicoDesenhoRepository
    {
        public readonly MelcContext _context;

        public ServicoDesenhoRepository(MelcContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DesenhoServico>> GetAllAsync()
        {
            return await _context.Servicos.ToListAsync();
        }

        public async Task<DesenhoServico> GetByIdAsync(Guid id)
        {
            return await _context.Servicos
                .Include(x => x.CriadoPor)
                .FirstAsync(x => x.Id == id);
        }

        public async Task<Guid> InsertAsync(DesenhoServico entity)
        {
            var resultado = await _context.Servicos.AddAsync(entity);

            await _context.SaveChangesAsync();

            return resultado.Entity.Id;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var entity = await _context.Servicos.FindAsync(id);

            _context.Servicos.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task Update(DesenhoServico entity)
        {
            _context.Servicos.Update(entity);

            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
