using MELC.WebApi.Core.Identidade;
using MELC.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace MELC.WebApp.MVC.Controllers
{
    public class ArquivoDesenhoController : BaseController
    {
        private readonly IArquivoDesenhoService _arquivoDesenhoService;

        public ArquivoDesenhoController(IArquivoDesenhoService ArquivoDesenhoervice)
        {
            _arquivoDesenhoService = ArquivoDesenhoervice;
        }

        [HttpDelete]
        [Route("ArquivoDesenho/Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var resultado = await _arquivoDesenhoService.DeleteAsync(id);

            if (!resultado.Success)
                return Json(new { success = false, data = resultado.ResponseResult.Errors.Messages });

            return Json(new { success = true});
        }

        [HttpGet]
        [Route("ArquivoDesenho/Get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var arquivoDesenho = await _arquivoDesenhoService.GetByIdAsync(id);

                if (!arquivoDesenho.Success)
                    return Json(new { success = false, data = arquivoDesenho.ResponseResult.Errors.Messages });

                return Json(new { success = true, data = arquivoDesenho.Data });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
