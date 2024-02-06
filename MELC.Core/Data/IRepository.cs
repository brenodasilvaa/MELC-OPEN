using MELC.Core.DomainObjects;
using MELC.Core.DomainObjects.Dtos;

namespace MELC.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<Guid> InsertAsync(T entity);
        Task<T> GetByIdAsync(Guid id);
        Task DeleteByIdAsync(Guid id);
        Task Update(T entity);
    }
}
