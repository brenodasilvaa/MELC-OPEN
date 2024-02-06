using Microsoft.Extensions.Options;
using MELC.WebApp.MVC.Extensions;
using MELC.Core.DomainObjects.Dtos;
using MELC.WebApp.MVC.Models;
using MELC.WebApp.MVC.Models.Faturamentos;
using MELC.Core.Commons.Enums;
using MELC.Core.Commons;
using MELC.Core.Commons.FileHelper;

namespace MELC.WebApp.MVC.Services
{
    public class DesenhosService : Service, IDesenhosService
    {
        private readonly HttpClient _httpClient;
        private readonly string _pathArquivos = FileHelper.GetPathArquivos("Desenhos");

        public DesenhosService(HttpClient httpClient, 
                                    IOptions<AppSettings> options)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(options.Value.MainApiUrl);
        }

        public async Task<RetornoDto<Guid>> CreateDesenho(DesenhoDto newDesenho)
        {
            var DesenhoContent = ObterConteudo(newDesenho);

            var response = await _httpClient.PostAsync($"/api/desenhos/new-desenho", DesenhoContent);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<Guid> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<Guid> { Data = ObterObjeto<Guid>(responseContent) };
        }

        public async Task<RetornoDto<bool>> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/desenhos/delete-by-id/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<bool> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<bool> { Data = ObterObjeto<bool>(responseContent) };
        }

        public async Task<RetornoDto<IEnumerable<DesenhoDto>>> GetAllDesenhosAsync()
        {
            var response = await _httpClient.GetAsync($"/api/desenhos/getAll");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<IEnumerable<DesenhoDto>> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<IEnumerable<DesenhoDto>> { Data = ObterObjeto<IEnumerable<DesenhoDto>>(responseContent) };
        }

        public async Task<RetornoDto<IEnumerable<DesenhoDto>>> GetDesenhosByPedidoIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/desenhos/getAllByPedidoId/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<IEnumerable<DesenhoDto>> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<IEnumerable<DesenhoDto>> {Success = true, Data = ObterObjeto<IEnumerable<DesenhoDto>>(responseContent) };
        }

        public async Task<RetornoDto<bool>> SalvarArquivosNovoDesenho(IEnumerable<IFormFile> formFiles, Guid desenhoId)
        {
            var pathDesenho = Path.Combine(_pathArquivos, desenhoId.ToString());

            var resultado = await SalvarArquivosAsync(formFiles, desenhoId, pathDesenho);

            if (!resultado.Success)
            {
                await DeleteAsync(desenhoId);

                Directory.Delete(pathDesenho, true);
            }

            return resultado;
        }

        private async Task<RetornoDto<bool>> SalvarArquivosAsync(IEnumerable<IFormFile> formFiles, Guid desenhoId, string pathDesenho)
        {
            if (formFiles is null)
                formFiles = new List<IFormFile>();

            var arquivosDesenho = new List<ArquivoDesenhoDto>();

            try
            {
                foreach (var item in formFiles)
                {
                    var filePath = Path.Combine(pathDesenho, item.FileName);

                    using (var file = item.OpenReadStream())
                        filePath = await FileHelper.CopyStreamToFileAsync(pathDesenho, file, filePath);

                    arquivosDesenho.Add(
                            new ArquivoDesenhoDto
                            {
                                CaminhoArquivo = filePath,
                                NomeArquivo = Path.GetFileName(filePath),
                                DesenhoId = desenhoId,
                                Created = DateTime.Now
                            });
                }

                if (arquivosDesenho.Any())
                {
                    var resultado = await InsereArquivosNoBanco(arquivosDesenho);

                    if (!resultado.Success)
                    {
                        return resultado;
                    }
                }

                return new RetornoDto<bool> { Success = true };
            }
            catch
            {
                return new RetornoDto<bool> { Success = false, Message = "Não foi possível salvar os arquivos" };
            }
        }

        private async Task<RetornoDto<string>> ExcluirArquivoAsync(Guid arquivoId, Guid desenhoId, string pathDesenho)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/desenhos/delete-arquivo/{arquivoId}");

                var responseContent = await response.Content.ReadAsStringAsync();

                if (!TratarErrosResponse(response))
                {
                    return new RetornoDto<string> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
                }

                var nomeArquivo = ObterObjeto<string>(responseContent);

                return new RetornoDto<string> { Success = true, Data = nomeArquivo };
            }
            catch
            {
                return new RetornoDto<string> { Success = false, Message = "Não foi possível salvar os arquivos" };
            }
        }

        private async Task<RetornoDto<bool>> InsereArquivosNoBanco(IEnumerable<ArquivoDesenhoDto> arquivosDesenhoDto)
        {
            var arquivoDesenhoContent = ObterConteudo(arquivosDesenhoDto);

            var response = await _httpClient.PostAsync($"/api/desenhos/novos-arquivos", arquivoDesenhoContent);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<bool> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<bool> {Success = true, Data = ObterObjeto<bool>(responseContent) };
        }

        public async Task<RetornoDto<DesenhoDto>> GetDesenhosByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/desenhos/getById/{id}");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<DesenhoDto> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<DesenhoDto> { Data = ObterObjeto<DesenhoDto>(responseContent) };
        }

        public async Task<RetornoDto<bool>> SalvarArquivos(IEnumerable<IFormFile> formFiles, Guid desenhoId)
        {
            var pathDesenho = Path.Combine(_pathArquivos, desenhoId.ToString());

            return await SalvarArquivosAsync(formFiles, desenhoId, pathDesenho);
        }

        public async Task<RetornoDto<string>> ExcluirArquivo(Guid arquivoId, Guid desenhoId)
        {
            var pathDesenho = Path.Combine(_pathArquivos, desenhoId.ToString());

            return await ExcluirArquivoAsync(arquivoId, desenhoId, pathDesenho);
        }

        public async Task<RetornoDto<bool>> UpdateInfo(UpdateInfoModel updateInfo)
        {
            var desenho = new DesenhoDto
            {
                Id = updateInfo.Id,
                Conjunto = updateInfo.Conjunto,
                Descricao = updateInfo.Descricao ?? string.Empty,
                NumeroConjunto = updateInfo.NumeroConjunto,
                Quantidade = updateInfo.Quantidade,
                Status = updateInfo.Status,
                Prioridade = updateInfo.Prioridade
            };

            if (updateInfo.Status == Status.Billed || updateInfo.Status == Status.Finished)
                desenho.Prioridade = 0;

            var conteudoDesenho = ObterConteudo(desenho);

            var responseUpdate = await _httpClient.PostAsync($"/api/desenhos/update", conteudoDesenho);

            var responseContentUpdate = await responseUpdate.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(responseUpdate))
            {
                return new RetornoDto<bool> { ResponseResult = ObterObjeto<ResponseResult>(responseContentUpdate) };
            }

            return new RetornoDto<bool> { Success = true, Data = ObterObjeto<bool>(responseContentUpdate) };
        }

        public async Task<RetornoDto<bool>> UpdateLucrosImpostos(UpdateLucrosImpostosModel updateLucrosImpostos)
        {
            var desenho = new DesenhoDto
            {
                Id = updateLucrosImpostos.Id,
                Impostos = updateLucrosImpostos.Impostos,
                Lucro = updateLucrosImpostos.Lucro
            };

            var conteudoDesenho = ObterConteudo(desenho);

            var responseUpdate = await _httpClient.PostAsync($"/api/desenhos/update-lucros-impostos", conteudoDesenho);

            var responseContentUpdate = await responseUpdate.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(responseUpdate))
            {
                return new RetornoDto<bool> { ResponseResult = ObterObjeto<ResponseResult>(responseContentUpdate) };
            }

            return new RetornoDto<bool> { Success = true, Data = ObterObjeto<bool>(responseContentUpdate) };
        }
        public async Task<RetornoDto<bool>> InserirServico(ServicoDesenhoDto desenhoServico)
        {

            var conteudoServico = ObterConteudo(desenhoServico);

            var responseServico = await _httpClient.PostAsync($"/api/desenhos/inserir-servico", conteudoServico);

            var responseContentServico = await responseServico.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(responseServico))
            {
                return new RetornoDto<bool> { ResponseResult = ObterObjeto<ResponseResult>(responseContentServico) };
            }

            return new RetornoDto<bool> { Success = true, Data = ObterObjeto<bool>(responseContentServico) };
        }

        public async Task<RetornoDto<IEnumerable<MaterialDto>>> GetMateriais()
        {
            var response = await _httpClient.GetAsync($"/api/desenhos/get-materiais");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<IEnumerable<MaterialDto>> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<IEnumerable<MaterialDto>> { Success = true, Data = ObterObjeto<IEnumerable<MaterialDto>>(responseContent) };
        }

        public async Task<RetornoDto<bool>> InserirMaterialDesenho(MaterialDesenhoDto materialDesenho)
        {
            var conteudoMaterial = ObterConteudo(materialDesenho);

            var responseMaterialDesenho = await _httpClient.PostAsync($"/api/desenhos/inserir-material-desenho", conteudoMaterial);

            var responseContentMaterialDesenho = await responseMaterialDesenho.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(responseMaterialDesenho))
            {
                return new RetornoDto<bool> { ResponseResult = ObterObjeto<ResponseResult>(responseContentMaterialDesenho) };
            }

            return new RetornoDto<bool> { Success = true, Data = ObterObjeto<bool>(responseContentMaterialDesenho) };
        }

        public async Task<RetornoDto<IEnumerable<FaturamentoAgrupador>>> GetDesenhosFaturamento(IEnumerable<DesenhoDto> desenhos, Status status)
        {
            var listaDesenhos = desenhos.Where(x => x.Status == status);

            var listaDesenhosFaturamento = new List<FaturamentoDesenhoModel>();

            foreach (var desenho in listaDesenhos)
            {
                var ready = desenho.PecasNormalizadas.All(x => x.Preco != 0) &&
                            desenho.MateriaisDesenhos.All(x => x.Material.Preco != 0) &&
                            desenho.DesenhoServicos.All(x => x.TipoServico.Valor != 0);

                var desenhoFaturamento = new FaturamentoDesenhoModel
                {
                    Title = desenho.Title,
                    NumeroDesenho = desenho.NumeroDesenho,
                    Id = desenho.Id,
                    Conjunto = desenho.Conjunto,
                    NumeroConjunto = desenho.NumeroConjunto,
                    Ready = ready
                };

                listaDesenhosFaturamento.Add(desenhoFaturamento);
            }

            var groupByConjuntoNumeroConjunto = listaDesenhosFaturamento.GroupBy(x => $"{x.Conjunto} {x.NumeroConjunto}");
            var listaFaturamentoAgrupador = new List<FaturamentoAgrupador>();

            foreach (var grupo in groupByConjuntoNumeroConjunto)
            {
                var faturamentoAgrupador = new FaturamentoAgrupador
                {
                    Agrupador = string.IsNullOrWhiteSpace(grupo.Key) ? "Sem conjunto" : grupo.Key,
                    Faturamentos = grupo,
                    Id = Guid.NewGuid()
                };

                listaFaturamentoAgrupador.Add(faturamentoAgrupador);
            };

            return new RetornoDto<IEnumerable<FaturamentoAgrupador>>
            {
                Success = true,
                Data = listaFaturamentoAgrupador
            };
        }

        public async Task<RetornoDto<IEnumerable<DesenhoDto>>> GetDesenhosByPrioridadeAsync()
        {
            var response = await _httpClient.GetAsync($"/api/desenhos/get-all-by-prioridade");

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!TratarErrosResponse(response))
            {
                return new RetornoDto<IEnumerable<DesenhoDto>> { ResponseResult = ObterObjeto<ResponseResult>(responseContent) };
            }

            return new RetornoDto<IEnumerable<DesenhoDto>> { Success = true, Data = ObterObjeto<IEnumerable<DesenhoDto>>(responseContent) };
        }
    }
}
