using AutoMapper;
using MELC.Core.Commons.FileHelper;
using MELC.Core.DomainObjects.Dtos;
using MELC.Main.API.Data.Repository;
using MELC.Main.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MELC.Main.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/arquivo-desenho")]
    public class ArquivoDesenhoController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IArquivoDesenhoRepository _arquivoDesenhoRepository;
        public ArquivoDesenhoController(
            IMapper mapper,
            IArquivoDesenhoRepository ArquivoDesenhoRepository,
            IDesenhoRepository desenhoRepository)
        {
            _mapper = mapper;
            _arquivoDesenhoRepository = ArquivoDesenhoRepository;
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var arquivoDesenho = _mapper.Map<ArquivoDesenho, ArquivoDesenhoDto>(await _arquivoDesenhoRepository.GetByIdAsync(id));

            arquivoDesenho.Base64 = FileHelper.ConvertPdfToBase64(arquivoDesenho.CaminhoArquivo);
            
            return CustomResponse(arquivoDesenho);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {

            var arquivoDesenho = _mapper.Map<ArquivoDesenho, ArquivoDesenhoDto>(await _arquivoDesenhoRepository.GetByIdAsync(id));

            await _arquivoDesenhoRepository.DeleteByIdAsync(id);

            System.IO.File.Delete(arquivoDesenho.CaminhoArquivo);

            return Ok(true);
        }
    }
}
