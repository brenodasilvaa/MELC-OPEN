using MELC.Core.DomainObjects.Dtos;
using MELC.WebApp.MVC.Extensions;
using Microsoft.Extensions.Options;

namespace MELC.WebApp.MVC.Services
{
    public class PercentuaisService : Service, IPercentuaisService
    {
        private readonly HttpClient _httpClient;

        public PercentuaisService(HttpClient httpClient,
                                    IOptions<AppSettings> options)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(options.Value.MainApiUrl);
        }
        public async Task<RetornoDto<Guid>> CreateAsync(PercentuaisDto newPercentuais)
        {
            var PercentuaisContent = ObterConteudo(newPercentuais);

            var response = await _httpClient.PostAsync($"/api/percentuais/new", PercentuaisContent);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<Guid> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<Guid> { Success = true, Data = ObterObjeto<Guid>(responseContent) };
        }

        public async Task<RetornoDto<bool>> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/percentuais/delete/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<bool> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<bool> { Data = ObterObjeto<bool>(responseContent) };
        }

        public async Task<RetornoDto<IEnumerable<PercentuaisDto>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"/api/percentuais/get-all");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<IEnumerable<PercentuaisDto>> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<IEnumerable<PercentuaisDto>> {Success = true, Data = ObterObjeto<IEnumerable<PercentuaisDto>>(responseContent) };
        }

        public async Task<RetornoDto<PercentuaisDto>> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/percentuais/get-by-Id/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<PercentuaisDto> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<PercentuaisDto> { Success = true, Data = ObterObjeto<PercentuaisDto>(responseContent) };
        }

        public async Task<RetornoDto<bool>> UpdateAsync(PercentuaisDto Percentuais)
        {
            var conteudoPercentuais = ObterConteudo(Percentuais);

            var responseUpdate = await _httpClient.PostAsync($"/api/percentuais/update", conteudoPercentuais);

            var responseContentUpdate = await responseUpdate.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(responseUpdate))
            {
                return new RetornoDto<bool> { ResponseResult = ObterObjeto<ResponseResult>(responseContentUpdate) };
            }

            return new RetornoDto<bool> { Success = true, Data = ObterObjeto<bool>(responseContentUpdate) };
        }
    }
}
