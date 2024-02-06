using MELC.Core.Commons.FileHelper;
using MELC.Core.DomainObjects.Dtos;
using MELC.PDF.Facade;
using MELC.WebApp.MVC.Extensions;
using Microsoft.Extensions.Options;

namespace MELC.WebApp.MVC.Services
{
    public class ArquivoDesenhoService : Service, IArquivoDesenhoService
    {
        private readonly HttpClient _httpClient;

        public ArquivoDesenhoService(HttpClient httpClient,
                                    IOptions<AppSettings> options)
        {

            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(options.Value.MainApiUrl);
        }

        public async Task<RetornoDto<bool>> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/arquivo-desenho/delete/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<bool> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<bool> { Success = true, Data = ObterObjeto<bool>(responseContent) };
        }

        public async Task<RetornoDto<ArquivoDesenhoDto>> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/arquivo-desenho/get-by-Id/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<ArquivoDesenhoDto> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<ArquivoDesenhoDto> { Success = true, Data = ObterObjeto<ArquivoDesenhoDto>(responseContent) };
        }
    }
}
