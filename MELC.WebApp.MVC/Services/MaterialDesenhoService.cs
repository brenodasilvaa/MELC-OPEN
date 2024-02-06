using MELC.Core.DomainObjects.Dtos;
using MELC.WebApp.MVC.Extensions;
using Microsoft.Extensions.Options;

namespace MELC.WebApp.MVC.Services
{
    public class MaterialDesenhoService : Service, IMaterialDesenhoService
    {
        private readonly HttpClient _httpClient;

        public MaterialDesenhoService(HttpClient httpClient,
                                    IOptions<AppSettings> options)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(options.Value.MainApiUrl);
        }
        public async Task<RetornoDto<Guid>> CreateAsync(MaterialDesenhoDto newMaterialDesenho)
        {
            var MaterialDesenhoContent = ObterConteudo(newMaterialDesenho);

            var response = await _httpClient.PostAsync($"/api/material-desenho/new", MaterialDesenhoContent);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<Guid> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<Guid> { Success = true, Data = ObterObjeto<Guid>(responseContent) };
        }

        public async Task<RetornoDto<bool>> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/material-desenho/delete/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<bool> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<bool> { Data = ObterObjeto<bool>(responseContent) };
        }

        public async Task<RetornoDto<IEnumerable<MaterialDesenhoDto>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"/api/material-desenho/get-all");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<IEnumerable<MaterialDesenhoDto>> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<IEnumerable<MaterialDesenhoDto>> {Success = true, Data = ObterObjeto<IEnumerable<MaterialDesenhoDto>>(responseContent) };
        }

        public async Task<RetornoDto<MaterialDesenhoDto>> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/material-desenho/get-by-Id/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<MaterialDesenhoDto> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<MaterialDesenhoDto> { Success = true, Data = ObterObjeto<MaterialDesenhoDto>(responseContent) };
        }

        public async Task<RetornoDto<bool>> UpdateAsync(MaterialDesenhoDto MaterialDesenho)
        {
            var conteudoMaterialDesenho = ObterConteudo(MaterialDesenho);

            var responseUpdate = await _httpClient.PostAsync($"/api/material-desenho/update", conteudoMaterialDesenho);

            var responseContentUpdate = await responseUpdate.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(responseUpdate))
            {
                return new RetornoDto<bool> { ResponseResult = ObterObjeto<ResponseResult>(responseContentUpdate) };
            }

            return new RetornoDto<bool> { Success = true, Data = ObterObjeto<bool>(responseContentUpdate) };
        }
    }
}
