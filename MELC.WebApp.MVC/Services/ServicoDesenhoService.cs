using MELC.Core.DomainObjects.Dtos;
using MELC.WebApp.MVC.Extensions;
using Microsoft.Extensions.Options;

namespace MELC.WebApp.MVC.Services
{
    public class ServicoDesenhoService : Service, IServicoDesenhoService
    {
        private readonly HttpClient _httpClient;

        public ServicoDesenhoService(HttpClient httpClient,
                                    IOptions<AppSettings> options)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(options.Value.MainApiUrl);
        }
        public async Task<RetornoDto<Guid>> CreateAsync(ServicoDesenhoDto newServicoDesenho)
        {
            var ServicoDesenhoContent = ObterConteudo(newServicoDesenho);

            var response = await _httpClient.PostAsync($"/api/servico-desenho/new", ServicoDesenhoContent);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<Guid> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<Guid> { Success = true, Data = ObterObjeto<Guid>(responseContent) };
        }

        public async Task<RetornoDto<bool>> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/servico-desenho/delete/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<bool> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<bool> { Data = ObterObjeto<bool>(responseContent) };
        }

        public async Task<RetornoDto<IEnumerable<ServicoDesenhoDto>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"/api/servico-desenho/get-all");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<IEnumerable<ServicoDesenhoDto>> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<IEnumerable<ServicoDesenhoDto>> {Success = true, Data = ObterObjeto<IEnumerable<ServicoDesenhoDto>>(responseContent) };
        }

        public async Task<RetornoDto<ServicoDesenhoDto>> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/servico-desenho/get-by-Id/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<ServicoDesenhoDto> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<ServicoDesenhoDto> { Success = true, Data = ObterObjeto<ServicoDesenhoDto>(responseContent) };
        }

        public async Task<RetornoDto<bool>> UpdateAsync(ServicoDesenhoDto ServicoDesenho)
        {
            var conteudoServicoDesenho = ObterConteudo(ServicoDesenho);

            var responseUpdate = await _httpClient.PostAsync($"/api/servico-desenho/update", conteudoServicoDesenho);

            var responseContentUpdate = await responseUpdate.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(responseUpdate))
            {
                return new RetornoDto<bool> { ResponseResult = ObterObjeto<ResponseResult>(responseContentUpdate) };
            }

            return new RetornoDto<bool> { Success = true, Data = ObterObjeto<bool>(responseContentUpdate) };
        }
    }
}
