using MELC.Core.DomainObjects.Dtos;
using MELC.WebApp.MVC.Models;
using MELC.WebApp.MVC.Models.Pedidos;

namespace MELC.WebApp.MVC.Services
{
    public interface IPedidosService
    {
        Task<RetornoDto<IEnumerable<PedidoDto>>> GetAllPedidosAsync();
        Task<RetornoDto<IEnumerable<PedidoDto>>> GetPedidosByClienteId(Guid id);
        Task<RetornoDto<PedidoDto>> GetPedidosByIdAsync(Guid id);
        Task<RetornoDto<Guid>> CreatePedidoAsync(PedidoDto newPedido);
        Task<RetornoDto<bool>> DeleteAsync(Guid id);
        Task<RetornoDto<bool>> UpdateInfo(UpdateInfoModel updateInfoModel);
    }
}
