using Microsoft.EntityFrameworkCore;
using MELC.Main.API.Models;
using MELC.Core.Data;

namespace MELC.Main.API.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        public readonly MelcContext _context;

        public UserRepository(MelcContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<Guid> InsertAsync(User novoUser)
        {
            var resultado = await _context.Users.AddAsync(novoUser);

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
            throw new NotImplementedException();
        }

        public async Task Update(User entity)
        {
            _context.Users.Update(entity);

            await _context.SaveChangesAsync();
        }
    }
}
