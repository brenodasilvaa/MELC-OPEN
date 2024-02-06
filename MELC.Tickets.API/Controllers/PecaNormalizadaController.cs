using AutoMapper;
using MELC.Core.DomainObjects.Dtos;
using MELC.Main.API.Data.Repository;
using MELC.Main.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MELC.Main.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/peca-normalizada")]
    public class PecaNormalizadaController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IPecaNormalizadaRepository _pecaNormalizadaRepository;
        public PecaNormalizadaController(
            IMapper mapper,
            IPecaNormalizadaRepository PecaNormalizadaRepository)
        {
            _mapper = mapper;
            _pecaNormalizadaRepository = PecaNormalizadaRepository;
        }
        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return CustomResponse(_mapper.Map<IEnumerable<PecaNormalizada>, IEnumerable<PecaNormalizadaDto>>(await _pecaNormalizadaRepository.GetAllAsync()));
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            return CustomResponse(_mapper.Map<PecaNormalizada, PecaNormalizadaDto>(await _pecaNormalizadaRepository.GetByIdAsync(id)));
        }

        [HttpPost("new")]
        public async Task<ActionResult> New(PecaNormalizadaDto newPecaNormalizada)
        {
            return CustomResponse(await _pecaNormalizadaRepository.InsertAsync(_mapper.Map<PecaNormalizadaDto, PecaNormalizada>(newPecaNormalizada)));
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _pecaNormalizadaRepository.DeleteByIdAsync(id);

            return Ok(true);
        }

        [HttpPost("update")]
        public async Task<ActionResult> Update(PecaNormalizadaDto pecaNormalizada)
        {
            var peca = await _pecaNormalizadaRepository.GetByIdAsync(pecaNormalizada.Id);

            peca.Title = pecaNormalizada.Title;
            peca.Quantidade = pecaNormalizada.Quantidade;
            peca.Preco = pecaNormalizada.Preco;

            await _pecaNormalizadaRepository.Update(peca);

            return Ok(true);
        }
    }
}
