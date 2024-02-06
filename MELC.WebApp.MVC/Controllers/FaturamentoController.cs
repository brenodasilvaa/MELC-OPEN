using MELC.Core.DomainObjects.Dtos;
using MELC.WebApi.Core.Identidade;
using MELC.WebApp.MVC.Extensions;
using MELC.WebApp.MVC.Models.Faturamentos;
using MELC.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;

namespace MELC.WebApp.MVC.Controllers
{
    public class FaturamentoController : BaseController
    {
        private readonly IFaturamentoService _faturamentoService;

        public FaturamentoController(
            IFaturamentoService faturamentoervice)
        {
            _faturamentoService = faturamentoervice;
        }

        [HttpGet]
        [ClaimsAuthorize("Admin", "Admin")]
        [Route("Faturamento")]
        public async Task<IActionResult> Index()
        {
            var servicos = await _faturamentoService.GetAllAsync();

            if (servicos == null)
                return NotFound();

            return View("~/Views/Faturamento/Index.cshtml", servicos);
        }


        [HttpPost]
        [ClaimsAuthorize("Admin", "Admin")]
        [Route("Faturamento/Create")]
        public async Task<IActionResult> Create([FromForm] NewFaturamentoViewModel newFaturamento)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Json(new
                    {
                        success = false,
                        data =
                        ModelState.Values.SelectMany(v => v.Errors.Select(y => y.ErrorMessage))
                    });

                if (!newFaturamento.DesenhosIds.Any())
                    return Json(new { success = false, data = "Nenhuma peça foi selecionada para gerar faturamento" });

                var faturamentoDto = new FaturamentoDto
                {
                    Title = newFaturamento.GetTitle(),
                    Extensao = "pdf",
                    NomeArquivo = newFaturamento.FileName,
                    DesenhosIds = newFaturamento.DesenhosIds,
                    CriadoPorId = Guid.Parse(User.GetUserId()),
                    Pecas = string.Join(";", newFaturamento.DesenhosIds)
                };

                var resultado = await _faturamentoService.CreateAsync(faturamentoDto);

                if (!resultado.Success)
                    return Json(new { success = false, data = resultado.ResponseResult.Errors.Messages});

                return Json(new { success = true });
            }
            catch(Exception ex)
            {
                return Json(new { success = false, data = "Não foi possível realizar esta operação" });
            }
        }

        [HttpDelete]
        [ClaimsAuthorize("Admin", "Admin")]
        [Route("Faturamento/Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var resultado = await _faturamentoService.DeleteAsync(id);

            if (ValidarResposta(resultado.ResponseResult)) return BadRequest(resultado.ResponseResult);

            return Ok(true);
        }

        [HttpPost]
        [ClaimsAuthorize("Admin", "Admin")]
        [Route("Faturamento/Update")]
        public async Task<IActionResult> Update(FaturamentoDto Faturamento)
        {
            try
            {
                var servico = await _faturamentoService.UpdateAsync(Faturamento);

                if (ValidarResposta(servico.ResponseResult))
                    return Json(new
                    {
                        success = false,
                        data = Erros
                    });

                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false, data = "Não foi possível realizar esta operação" });
            }
        }

        [HttpGet]
        [ClaimsAuthorize("Admin", "Admin")]
        [Route("Faturamento/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var faturamento = await _faturamentoService.GetAllAsync();

            if (!faturamento.Success)
                return Json(new { success = false, data = faturamento.ResponseResult.Errors.Messages });

            return Json(new { success = true, data = faturamento.Data });
        }

        [HttpGet]
        [ClaimsAuthorize("Admin", "Admin")]
        [Route("Faturamento/Get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var faturamento = await _faturamentoService.GetByIdAsync(id);

                if (!faturamento.Success)
                    return Json(new { success = false, data = faturamento.ResponseResult.Errors.Messages });

                return Json(new { success = true, data = faturamento.Data });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
