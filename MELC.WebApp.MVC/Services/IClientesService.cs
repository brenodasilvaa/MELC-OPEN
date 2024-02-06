using MELC.Core.DomainObjects.Dtos;
using MELC.WebApp.MVC.Models;
using MELC.WebApp.MVC.Models.Clientes;

namespace MELC.WebApp.MVC.Services
{
    public interface IClientesService
    {
        Task<RetornoDto<IEnumerable<ClienteViewModel>>> GetAllClientesAsync();
        Task<RetornoDto<ClienteDto>> GetClienteByIdAsync(Guid id);
        Task<RetornoDto<Guid>> CreateClienteAsync(ClienteDto newCliente);
        Task<RetornoDto<bool>> DeleteAsync(Guid id);
        Task<RetornoDto<bool>> Update(ClienteDto clienteDto);
    }
}
