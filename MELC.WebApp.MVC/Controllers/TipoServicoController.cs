using MELC.Core.DomainObjects.Dtos;
using MELC.WebApi.Core.Identidade;
using MELC.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace MELC.WebApp.MVC.Controllers
{
    public class TipoServicosController : BaseController
    {
        private readonly ITipoServicoService _tipoServicoService;

        public TipoServicosController(
            ITipoServicoService tipoServicoService)
        {
            _tipoServicoService = tipoServicoService;
        }

        [HttpGet]
        [ClaimsAuthorize("Admin", "Admin")]
        [Route("TipoServico")]
        public async Task<IActionResult> Index()
        {
            var servicos = await _tipoServicoService.GetAllAsync();

            if (servicos == null)
                return NotFound();

            return View("~/Views/TipoServico/Index.cshtml", servicos);
        }


        [HttpPost]
        [ClaimsAuthorize("Admin", "Admin")]
        [Route("TipoServico/Create")]
        public async Task<IActionResult> Create([FromForm] TipoServicoDto newServico)
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

                var resultado = await _tipoServicoService.CreateAsync(newServico);

                if (!resultado.Success)
                    return Json(new { success = false, data = resultado.ResponseResult.Errors.Messages});

                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false, data = "Não foi possível realizar esta operação" });
            }
        }

        [HttpDelete]
        [ClaimsAuthorize("Admin", "Admin")]
        [Route("TipoServico/Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var resultado = await _tipoServicoService.DeleteAsync(id);

            if (ValidarResposta(resultado.ResponseResult)) return BadRequest(resultado.ResponseResult);

            return Ok(true);
        }

        [HttpPost]
        [ClaimsAuthorize("Admin", "Admin")]
        [Route("TipoServico/Update")]
        public async Task<IActionResult> Update(TipoServicoDto tipoServico)
        {
            var servico = await _tipoServicoService.UpdateAsync(tipoServico);

            if (ValidarResposta(servico.ResponseResult))
                return Json(new
                {
                    success = false,
                    data = Erros
                });

            return Json(new { success = true });
        }

        [HttpGet]
        [Route("TipoServico/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var tiposServicos = await _tipoServicoService.GetAllAsync();

            if (!tiposServicos.Success)
                return Json(new { success = false, data = tiposServicos.ResponseResult.Errors.Messages });

            return Json(new { success = true, data = tiposServicos.Data });
        }

        [HttpGet]
        [ClaimsAuthorize("Admin", "Admin")]
        [Route("TipoServico/Get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var tiposServicos = await _tipoServicoService.GetByIdAsync(id);

                if (!tiposServicos.Success)
                    return Json(new { success = false, data = tiposServicos.ResponseResult.Errors.Messages });

                return Json(new { success = true, data = tiposServicos.Data });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
