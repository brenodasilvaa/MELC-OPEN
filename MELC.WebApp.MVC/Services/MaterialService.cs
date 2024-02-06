using MELC.Core.DomainObjects.Dtos;
using MELC.WebApp.MVC.Extensions;
using Microsoft.Extensions.Options;

namespace MELC.WebApp.MVC.Services
{
    public class MaterialService : Service, IMaterialService
    {
        private readonly HttpClient _httpClient;

        public MaterialService(HttpClient httpClient,
                                    IOptions<AppSettings> options)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(options.Value.MainApiUrl);
        }
        public async Task<RetornoDto<Guid>> CreateAsync(MaterialDto newMaterial)
        {
            var MaterialContent = ObterConteudo(newMaterial);

            var response = await _httpClient.PostAsync($"/api/material/new", MaterialContent);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<Guid> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<Guid> { Success = true, Data = ObterObjeto<Guid>(responseContent) };
        }

        public async Task<RetornoDto<bool>> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/material/delete/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<bool> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<bool> { Data = ObterObjeto<bool>(responseContent) };
        }

        public async Task<RetornoDto<IEnumerable<MaterialDto>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"/api/material/get-all");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<IEnumerable<MaterialDto>> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<IEnumerable<MaterialDto>> {Success = true, Data = ObterObjeto<IEnumerable<MaterialDto>>(responseContent) };
        }

        public async Task<RetornoDto<MaterialDto>> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/material/get-by-Id/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<MaterialDto> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<MaterialDto> { Success = true, Data = ObterObjeto<MaterialDto>(responseContent) };
        }

        public async Task<RetornoDto<bool>> UpdateAsync(MaterialDto Material)
        {
            var conteudoMaterial = ObterConteudo(Material);

            var responseUpdate = await _httpClient.PostAsync($"/api/material/update", conteudoMaterial);

            var responseContentUpdate = await responseUpdate.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(responseUpdate))
            {
                return new RetornoDto<bool> { ResponseResult = ObterObjeto<ResponseResult>(responseContentUpdate) };
            }

            return new RetornoDto<bool> { Success = true, Data = ObterObjeto<bool>(responseContentUpdate) };
        }
    }
}
