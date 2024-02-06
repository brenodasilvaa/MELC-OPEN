using MELC.Core.DomainObjects.Dtos;
using MELC.WebApp.MVC.Extensions;
using Microsoft.Extensions.Options;

namespace MELC.WebApp.MVC.Services
{
    public class TipoServicoService : Service, ITipoServicoService
    {
        private readonly HttpClient _httpClient;

        public TipoServicoService(HttpClient httpClient,
                                    IOptions<AppSettings> options)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(options.Value.MainApiUrl);
        }
        public async Task<RetornoDto<Guid>> CreateAsync(TipoServicoDto newTipoServico)
        {
            var tipoServicoContent = ObterConteudo(newTipoServico);

            var response = await _httpClient.PostAsync($"/api/tipoServico/new", tipoServicoContent);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<Guid> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<Guid> { Success = true, Data = ObterObjeto<Guid>(responseContent) };
        }

        public async Task<RetornoDto<bool>> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/tipoServico/delete/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<bool> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<bool> { Data = ObterObjeto<bool>(responseContent) };
        }

        public async Task<RetornoDto<IEnumerable<TipoServicoDto>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"/api/tipoServico/get-all");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<IEnumerable<TipoServicoDto>> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<IEnumerable<TipoServicoDto>> {Success = true, Data = ObterObjeto<IEnumerable<TipoServicoDto>>(responseContent) };
        }

        public async Task<RetornoDto<TipoServicoDto>> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/tipoServico/get-by-Id/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<TipoServicoDto> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<TipoServicoDto> { Success = true, Data = ObterObjeto<TipoServicoDto>(responseContent) };
        }

        public async Task<RetornoDto<bool>> UpdateAsync(TipoServicoDto tipoServico)
        {
            var conteudoTipoServico = ObterConteudo(tipoServico);

            var responseUpdate = await _httpClient.PostAsync($"/api/tipoServico/update", conteudoTipoServico);

            var responseContentUpdate = await responseUpdate.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(responseUpdate))
            {
                return new RetornoDto<bool> { ResponseResult = ObterObjeto<ResponseResult>(responseContentUpdate) };
            }

            return new RetornoDto<bool> { Success = true, Data = ObterObjeto<bool>(responseContentUpdate) };
        }
    }
}
