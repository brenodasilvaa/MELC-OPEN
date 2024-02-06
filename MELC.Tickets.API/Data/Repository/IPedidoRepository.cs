using MELC.Core.Commons.Enums;
using MELC.Core.Data;
using MELC.Main.API.Models;

namespace MELC.Main.API.Data.Repository
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        Task<IEnumerable<Pedido>> GetByStatusAsync(Status status);
        Task<IEnumerable<Pedido>> GetByCriadoPorIdAsync(Guid userId);
        Task<IEnumerable<Pedido>> GetByClienteIdAsync(Guid userId);
        Task<bool> NumeroPedidoExiste(int userId);
    }
}
