using MELC.Core.DomainObjects.Dtos;
using MELC.WebApp.MVC.Extensions;
using Microsoft.Extensions.Options;

namespace MELC.WebApp.MVC.Services
{
    public class FreteDesenhoService : Service, IFreteDesenhoService
    {
        private readonly HttpClient _httpClient;

        public FreteDesenhoService(HttpClient httpClient,
                                    IOptions<AppSettings> options)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(options.Value.MainApiUrl);
        }
        public async Task<RetornoDto<Guid>> CreateAsync(FreteDesenhoDto newFreteDesenho)
        {
            var FreteDesenhoContent = ObterConteudo(newFreteDesenho);

            var response = await _httpClient.PostAsync($"/api/frete-desenho/new", FreteDesenhoContent);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<Guid> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<Guid> { Success = true, Data = ObterObjeto<Guid>(responseContent) };
        }

        public async Task<RetornoDto<bool>> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/frete-desenho/delete/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<bool> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<bool> { Data = ObterObjeto<bool>(responseContent) };
        }

        public async Task<RetornoDto<IEnumerable<FreteDesenhoDto>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"/api/frete-desenho/get-all");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<IEnumerable<FreteDesenhoDto>> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<IEnumerable<FreteDesenhoDto>> {Success = true, Data = ObterObjeto<IEnumerable<FreteDesenhoDto>>(responseContent) };
        }

        public async Task<RetornoDto<FreteDesenhoDto>> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/frete-desenho/get-by-Id/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<FreteDesenhoDto> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<FreteDesenhoDto> { Success = true, Data = ObterObjeto<FreteDesenhoDto>(responseContent) };
        }

        public async Task<RetornoDto<bool>> UpdateAsync(FreteDesenhoDto FreteDesenho)
        {
            var conteudoFreteDesenho = ObterConteudo(FreteDesenho);

            var responseUpdate = await _httpClient.PostAsync($"/api/frete-desenho/update", conteudoFreteDesenho);

            var responseContentUpdate = await responseUpdate.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(responseUpdate))
            {
                return new RetornoDto<bool> { ResponseResult = ObterObjeto<ResponseResult>(responseContentUpdate) };
            }

            return new RetornoDto<bool> { Success = true, Data = ObterObjeto<bool>(responseContentUpdate) };
        }
    }
}
