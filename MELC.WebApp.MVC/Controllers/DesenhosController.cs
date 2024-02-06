using MELC.Core.Commons.Enums;
using MELC.Core.DomainObjects.Dtos;
using MELC.WebApi.Core.Identidade;
using MELC.WebApp.MVC.Extensions;
using MELC.WebApp.MVC.Models;
using MELC.WebApp.MVC.Models.Desenhos;
using MELC.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace MELC.WebApp.MVC.Controllers
{
    public class DesenhosController : BaseController
    {
        private readonly IDesenhosService _desenhosService;
        private readonly IPedidosService _pedidoService;
        private readonly IClientesService _clientesService;
        private readonly IPercentuaisService _percentuaisService;

        public DesenhosController(
            IDesenhosService DesenhosService, 
            IPedidosService pedidoService,
            IClientesService clientesService,
            IPercentuaisService percentuaisService)
        {
            _desenhosService = DesenhosService;
            _pedidoService = pedidoService;
            _clientesService = clientesService;
            _percentuaisService = percentuaisService;
        }

        [HttpGet]
        [Route("Desenhos/Create/{id}")]
        public IActionResult Create(Guid id)
        {
            return View("~/Views/Desenhos/novo-desenho.cshtml", new NewDesenhoViewModel { PedidoId = id });
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm]NewDesenhoViewModel newDesenho)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, data = 
                    ModelState.Values.SelectMany(v => v.Errors.Select(y => y.ErrorMessage)) });

            var percentual = (await _percentuaisService.GetAllAsync()).Data.FirstOrDefault();

            var DesenhoDto = new DesenhoDto
            {
                Title = newDesenho.Title,
                NumeroDesenho = newDesenho.NumeroDesenho ?? 0,
                PedidoId = newDesenho.PedidoId,
                Conjunto = newDesenho.Conjunto,
                Quantidade = newDesenho.Quantidade == 0 ? 1 : newDesenho.Quantidade,
                NumeroConjunto = newDesenho.NumeroConjunto,
                Descricao = newDesenho.Descricao ?? string.Empty,
                CriadoPorId = Guid.Parse(User.GetUserId()),
                Lucro = percentual?.Lucro,
                Impostos = percentual?.Impostos
            };

            var resultado = await _desenhosService.CreateDesenho(DesenhoDto);

            if (ValidarResposta(resultado.ResponseResult))
                return Json(new { success = false, data = Erros });

            var resultadoArquivo = await _desenhosService.SalvarArquivosNovoDesenho(newDesenho.FormFiles, resultado.Data);

            if (!resultadoArquivo.Success)
                Erros.Add(resultado.Message);

            if (ValidarResposta(resultadoArquivo.ResponseResult))
                return Json(new { success = false, data = Erros });

            var pedido = await _pedidoService.GetPedidosByIdAsync(newDesenho.PedidoId);

            if (ValidarResposta(pedido.ResponseResult))
                return Json(new {success = false, data = Erros });

            var cliente = await _clientesService.GetClienteByIdAsync(pedido.Data.ClienteId);

            if (ValidarResposta(cliente.ResponseResult))
                return Json(new { success = false, data = Erros });

            var desenhos = await _desenhosService.GetDesenhosByPedidoIdAsync(pedido.Data.Id);

            if (ValidarResposta(desenhos.ResponseResult))
                return Json(new { success = false, data = Erros });

            return Json(new { success = true });
        }

        [HttpDelete]
        [ClaimsAuthorize("Admin", "Admin")]
        [Route("Desenhos/Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var resultado = await _desenhosService.DeleteAsync(id);

                if (ValidarResposta(resultado.ResponseResult))
                    return Json(new { success = false, message = resultado.Message });

                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false, message = "Não foi possível realizar esta operação" });
            }
        }

        [HttpGet]
        [Route("Desenhos/GetDetalhe/{id}")]
        public async Task<IActionResult> GetDetalhe(Guid id)
        {
            var desenho = await _desenhosService.GetDesenhosByIdAsync(id);

            if (ValidarResposta(desenho.ResponseResult)) return View("~/Views/Desenhos");

            desenho.Data.AtualizarResumo();

            var pedido = await _pedidoService.GetPedidosByIdAsync(desenho.Data.PedidoId);

            if (ValidarResposta(pedido.ResponseResult)) return View("~/Views/Pedidos");

            var cliente = await _clientesService.GetClienteByIdAsync(pedido.Data.ClienteId);

            if (ValidarResposta(cliente.ResponseResult)) return View("~/Views/Pedidos");

            var desenhoViewModel = new RetornoDto<DesenhoViewModel>
            {
                Data = new DesenhoViewModel
                {
                    Cliente = cliente.Data,
                    Pedido = pedido.Data,
                    Desenho = desenho.Data,
                    Servicos = desenho.Data.DesenhoServicos,
                    Materiais = desenho.Data.MateriaisDesenhos,
                    PecasNormalizadas = desenho.Data.PecasNormalizadas,
                    Fretes = desenho.Data.FretesDesenhos,
                    Arquivos = desenho.Data.Arquivos
                }
            };

            return View("~/Views/Desenhos/desenho-detalhes.cshtml", desenhoViewModel);
        }


        [HttpGet]
        [Route("Desenhos/GetByPedidoIdFaturamento/{id}/{status}")]
        public async Task<IActionResult> GetByPedidoIdFaturamento(Guid id, Status status)
        {
            var resultadoArquivo = await _desenhosService.GetDesenhosByPedidoIdAsync(id);

            if (!resultadoArquivo.Success)
                return Json(new { success = false, data = resultadoArquivo.ResponseResult.Errors.Messages });

            var desenhos = await _desenhosService.GetDesenhosFaturamento(
                resultadoArquivo.Data, status);
            
            return Json(new {success = true, data = desenhos.Data});
        }

        [HttpPost]
        public async Task<IActionResult> SalvarArquivos(IEnumerable<IFormFile> formFiles, Guid desenhoId)
        {
            var resultadoArquivo = await _desenhosService.SalvarArquivos(formFiles, desenhoId);

            if (!resultadoArquivo.Success)
                return Json(new { success = false, data = resultadoArquivo.ResponseResult.Errors.Messages });

            return Json(new { success = true, message = $"/Desenhos/GetDetalhe/{desenhoId}" });
        }

        [HttpPost]
        public async Task<IActionResult> ExcluirArquivo(Guid arquivoId, Guid desenhoId)
        {
            var resultadoArquivo = await _desenhosService.ExcluirArquivo(arquivoId, desenhoId);

            if (!resultadoArquivo.Success)
                return Json(new { success = false, data = resultadoArquivo.ResponseResult.Errors.Messages });

            return Json(new { success = true, message = $"/Desenhos/GetDetalhe/{desenhoId}" });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateInfo(UpdateInfoModel updateInfo)
        {
            var resultado = await _desenhosService.UpdateInfo(updateInfo);

            if (!resultado.Success)
                return Json(new { success = false, data = resultado.ResponseResult.Errors.Messages });

            return Json(new { success = true });
        }


        [HttpPost]
        public async Task<IActionResult> UpdateLucrosImpostos(UpdateLucrosImpostosModel updateLucrosImpostos)
        {
            var resultado = await _desenhosService.UpdateLucrosImpostos(updateLucrosImpostos);

            if (!resultado.Success)
                return Json(new { success = false, data = resultado.ResponseResult.Errors.Messages });

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> InserirMaterial(NewMaterialDesenhoViewModel desenhoVm)
        {
            try
            {
                var materialDesenhoDto = new MaterialDesenhoDto
                {
                    DesenhoId = desenhoVm.DesenhoId,
                    CriadoPorId = Guid.Parse(User.GetUserId()),
                    MaterialId = desenhoVm.MaterialId,
                    Quantidade = desenhoVm.Quantidade,
                    Solido = new SolidoDto
                    {
                        Altura = desenhoVm.Altura,
                        Largura = desenhoVm.Largura,
                        Comprimento = desenhoVm.Comprimento,
                        Expessura = desenhoVm.Expessura,
                        ExpessuraSuperior = desenhoVm.ExpessuraSuperior,
                        Diametro = desenhoVm.Diametro,
                        TipoSolido = desenhoVm.TipoSolido
                    }
                };

                var resultado = await _desenhosService.InserirMaterialDesenho(materialDesenhoDto);

                if (!resultado.Success)
                    return Json(new { success = false, data = resultado.ResponseResult.Errors.Messages });

                return Json(new { success = true });
            }
            catch(Exception ex)
            {
                return Json(new { success = false, data = "Não foi possível realizar esta operação" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMateriais()
        {
            var materiais = await _desenhosService.GetMateriais();

            if (!materiais.Success)
                return Json(new { success = false, data = materiais.Message });

            return Json(new { success = true, data = materiais.Data });
        }

        [Route("Desenhos/Prioridades")]
        public async Task<IActionResult> Prioridades()
        {
            var desenhos = await _desenhosService.GetDesenhosByPrioridadeAsync();

            return View("~/Views/Desenhos/Prioridades.cshtml", desenhos);
        }
    }
}
