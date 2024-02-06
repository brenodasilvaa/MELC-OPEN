using MELC.Core.DomainObjects.Dtos;
using MELC.WebApi.Core.Identidade;
using MELC.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace MELC.WebApp.MVC.Controllers
{
    public class PercentuaisController : BaseController
    {
        private readonly IPercentuaisService _percentuaisService;

        public PercentuaisController(IPercentuaisService Percentuaiservice)
        {
            _percentuaisService = Percentuaiservice;
        }

        [HttpGet]
        [ClaimsAuthorize("Admin", "Admin")]
        [Route("Percentuais")]
        public async Task<IActionResult> Index()
        {
            var percentual = (await _percentuaisService.GetAllAsync()).Data.FirstOrDefault();

            var retornoPercentuais = new RetornoDto<PercentuaisDto>() { Data = new PercentuaisDto(), Success = true };

            if (percentual != null)
                retornoPercentuais = new RetornoDto<PercentuaisDto>() { Data = percentual, Success = true };

            return View("~/Views/Percentuais/Index.cshtml", retornoPercentuais);
        }


        [HttpPost]
        [ClaimsAuthorize("Admin", "Admin")]
        [Route("Percentuais/Create")]
        public async Task<IActionResult> Create([FromForm] PercentuaisDto newServico)
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

                var resultado = await _percentuaisService.CreateAsync(newServico);

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
        [Route("Percentuais/Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var resultado = await _percentuaisService.DeleteAsync(id);

            if (ValidarResposta(resultado.ResponseResult)) return BadRequest(resultado.ResponseResult);

            return Ok(true);
        }

        [HttpPost]
        [ClaimsAuthorize("Admin", "Admin")]
        [Route("Percentuais/Update")]
        public async Task<IActionResult> Update(PercentuaisDto Percentuais)
        {
            try
            {
                var servico = await _percentuaisService.UpdateAsync(Percentuais);

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
        [Route("Percentuais/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var Percentuais = await _percentuaisService.GetAllAsync();

            if (!Percentuais.Success)
                return Json(new { success = false, data = Percentuais.ResponseResult.Errors.Messages });

            return Json(new { success = true, data = Percentuais.Data });
        }

        [HttpGet]
        [ClaimsAuthorize("Admin", "Admin")]
        [Route("Percentuais/Get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var Percentuais = await _percentuaisService.GetByIdAsync(id);

                if (!Percentuais.Success)
                    return Json(new { success = false, data = Percentuais.ResponseResult.Errors.Messages });

                return Json(new { success = true, data = Percentuais.Data });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
