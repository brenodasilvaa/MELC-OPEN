using MELC.Core.DomainObjects.Dtos;
using MELC.WebApi.Core.Identidade;
using MELC.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace MELC.WebApp.MVC.Controllers
{
    public class MaterialController : BaseController
    {
        private readonly IMaterialService _materialService;

        public MaterialController(
            IMaterialService materialervice)
        {
            _materialService = materialervice;
        }

        [HttpGet]
        [ClaimsAuthorize("Admin", "Admin")]
        [Route("Material")]
        public async Task<IActionResult> Index()
        {
            var servicos = await _materialService.GetAllAsync();

            if (servicos == null)
                return NotFound();

            return View("~/Views/Material/Index.cshtml", servicos);
        }


        [HttpPost]
        [ClaimsAuthorize("Admin", "Admin")]
        [Route("Material/Create")]
        public async Task<IActionResult> Create([FromForm] MaterialDto newServico)
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

                var resultado = await _materialService.CreateAsync(newServico);

                if (!resultado.Success)
                    return Json(new { success = false, data = resultado.ResponseResult.Errors.Messages});

                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false, data = "Não foi possível realizar esta operação" });
            }
        }

        [HttpDelete]
        [ClaimsAuthorize("Admin", "Admin")]
        [Route("Material/Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var resultado = await _materialService.DeleteAsync(id);

            if (ValidarResposta(resultado.ResponseResult)) return BadRequest(resultado.ResponseResult);

            return Ok(true);
        }

        [HttpPost]
        [ClaimsAuthorize("Admin", "Admin")]
        [Route("Material/Update")]
        public async Task<IActionResult> Update(MaterialDto Material)
        {
            try
            {
                var servico = await _materialService.UpdateAsync(Material);

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
        [Route("Material/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var material = await _materialService.GetAllAsync();

            if (!material.Success)
                return Json(new { success = false, data = material.ResponseResult.Errors.Messages });

            return Json(new { success = true, data = material.Data });
        }

        [HttpGet]
        [ClaimsAuthorize("Admin", "Admin")]
        [Route("Material/Get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var material = await _materialService.GetByIdAsync(id);

                if (!material.Success)
                    return Json(new { success = false, data = material.ResponseResult.Errors.Messages });

                return Json(new { success = true, data = material.Data });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
