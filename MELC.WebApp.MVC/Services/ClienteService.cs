using Microsoft.Extensions.Options;
using MELC.WebApp.MVC.Extensions;
using MELC.Core.DomainObjects.Dtos;
using MELC.WebApp.MVC.Models.Clientes;
using MELC.WebApp.MVC.Models;
using System.Collections.Generic;

namespace MELC.WebApp.MVC.Services
{
    public class ClientesService : Service, IClientesService
    {
        private readonly HttpClient _httpClient;

        public ClientesService(HttpClient httpClient, 
                                    IOptions<AppSettings> options)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(options.Value.MainApiUrl);
        }

        public async Task<RetornoDto<IEnumerable<ClienteViewModel>>> GetAllClientesAsync()
        {
            var response = await _httpClient.GetAsync($"/api/clientes/getAll");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<IEnumerable<ClienteViewModel>> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<IEnumerable<ClienteViewModel>> { Data = ObterObjeto< IEnumerable<ClienteViewModel>> (responseContent) };
        }

        public async Task<RetornoDto<Guid>> CreateClienteAsync(ClienteDto newCliente)
        {
            newCliente.Cnpj = FormatarCnpj(newCliente.Cnpj);

            var ClienteContent = ObterConteudo(newCliente);

            var response = await _httpClient.PostAsync($"/api/clientes/new-cliente", ClienteContent);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<Guid> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<Guid> { Data = ObterObjeto<Guid>(responseContent) };
        }

        private static string FormatarCnpj(string cnpj)
        {
            return Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00");
        }

        public async Task<RetornoDto<ClienteDto>> GetClienteByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/clientes/get-by-id/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<ClienteDto> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<ClienteDto> {Success = true, Data = ObterObjeto<ClienteDto>(responseContent) };
        }

        public async Task<RetornoDto<bool>> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/clientes/delete-by-id/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<bool> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<bool> { Data = ObterObjeto<bool>(responseContent) };
        }

        public async Task<RetornoDto<bool>> Update(ClienteDto clienteDto)
        {
            clienteDto.Cnpj = FormatarCnpj(clienteDto.Cnpj);

            var clienteContent = ObterConteudo(clienteDto);

            var response = await _httpClient.PostAsync($"/api/clientes/update", clienteContent);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<bool> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<bool> { Data = ObterObjeto<bool>(responseContent) };
        }
    }
}
