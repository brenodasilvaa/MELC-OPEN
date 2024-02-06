using Microsoft.Extensions.Options;
using MELC.WebApp.MVC.Extensions;
using MELC.Core.DomainObjects.Dtos;
using MELC.WebApp.MVC.Models;

namespace MELC.WebApp.MVC.Services
{
    public class PedidosService : Service, IPedidosService
    {
        private readonly HttpClient _httpClient;

        public PedidosService(HttpClient httpClient, 
                                    IOptions<AppSettings> options)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(options.Value.MainApiUrl);
        }

        public async Task<RetornoDto<IEnumerable<PedidoDto>>> GetAllPedidosAsync()
        {
            var response = await _httpClient.GetAsync($"/api/pedidos/getAll");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<IEnumerable<PedidoDto>> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return ObterObjeto<RetornoDto<IEnumerable<PedidoDto>>>(responseContent);
        }

        public async Task<RetornoDto<Guid>> CreatePedidoAsync(PedidoDto newPedido)
        {
            var pedidoContent = ObterConteudo(newPedido);

            var response = await _httpClient.PostAsync($"/api/pedidos/new-pedido", pedidoContent);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<Guid> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<Guid> { Data = ObterObjeto<Guid>(responseContent) };
        }

        public async Task<RetornoDto<PedidoDto>> GetPedidosByIdAsync(Guid id)
        {

            var response = await _httpClient.GetAsync($"/api/pedidos/get-by-id/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<PedidoDto> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<PedidoDto> { Data = ObterObjeto<PedidoDto>(responseContent) };
        }

        public async Task<RetornoDto<IEnumerable<PedidoDto>>> GetPedidosByClienteId(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/pedidos/get-by-cliente-id/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<IEnumerable<PedidoDto>> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<IEnumerable<PedidoDto>> { Data = ObterObjeto<IEnumerable<PedidoDto>>(responseContent) };
        }

        public async Task<RetornoDto<bool>> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/pedidos/delete-by-id/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<bool> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<bool> { Data = ObterObjeto<bool>(responseContent) };
        }

        public async Task<RetornoDto<bool>> UpdateInfo(UpdateInfoModel updateInfo)
        {
            var desenho = new PedidoDto
            {
                Id = updateInfo.Id,
                Descricao = updateInfo.Descricao ?? string.Empty,
                Status = updateInfo.Status
            };

            var conteudoPedido = ObterConteudo(desenho);

            var responseUpdate = await _httpClient.PostAsync($"/api/pedidos/update", conteudoPedido);

            var responseContentUpdate = await responseUpdate.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(responseUpdate))
            {
                return new RetornoDto<bool> { ResponseResult = ObterObjeto<ResponseResult>(responseContentUpdate) };
            }

            return new RetornoDto<bool> { Success = true, Data = ObterObjeto<bool>(responseContentUpdate) };
        }
    }
}
