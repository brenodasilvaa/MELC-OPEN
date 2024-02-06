using MELC.Core.DomainObjects.Dtos;
using MELC.WebApp.MVC.Extensions;
using Microsoft.Extensions.Options;

namespace MELC.WebApp.MVC.Services
{
    public class PecaNormalizadaService : Service, IPecaNormalizadaService
    {
        private readonly HttpClient _httpClient;

        public PecaNormalizadaService(HttpClient httpClient,
                                    IOptions<AppSettings> options)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(options.Value.MainApiUrl);
        }
        public async Task<RetornoDto<Guid>> CreateAsync(PecaNormalizadaDto newPecaNormalizada)
        {
            var PecaNormalizadaContent = ObterConteudo(newPecaNormalizada);

            var response = await _httpClient.PostAsync($"/api/peca-normalizada/new", PecaNormalizadaContent);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<Guid> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<Guid> { Success = true, Data = ObterObjeto<Guid>(responseContent) };
        }

        public async Task<RetornoDto<bool>> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/peca-normalizada/delete/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<bool> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<bool> { Data = ObterObjeto<bool>(responseContent) };
        }

        public async Task<RetornoDto<IEnumerable<PecaNormalizadaDto>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"/api/peca-normalizada/get-all");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<IEnumerable<PecaNormalizadaDto>> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<IEnumerable<PecaNormalizadaDto>> {Success = true, Data = ObterObjeto<IEnumerable<PecaNormalizadaDto>>(responseContent) };
        }

        public async Task<RetornoDto<PecaNormalizadaDto>> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/peca-normalizada/get-by-Id/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<PecaNormalizadaDto> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<PecaNormalizadaDto> { Success = true, Data = ObterObjeto<PecaNormalizadaDto>(responseContent) };
        }

        public async Task<RetornoDto<bool>> UpdateAsync(PecaNormalizadaDto PecaNormalizada)
        {
            var conteudoPecaNormalizada = ObterConteudo(PecaNormalizada);

            var responseUpdate = await _httpClient.PostAsync($"/api/peca-normalizada/update", conteudoPecaNormalizada);

            var responseContentUpdate = await responseUpdate.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(responseUpdate))
            {
                return new RetornoDto<bool> { ResponseResult = ObterObjeto<ResponseResult>(responseContentUpdate) };
            }

            return new RetornoDto<bool> { Success = true, Data = ObterObjeto<bool>(responseContentUpdate) };
        }
    }
}
