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
    [Route("api/tipoServico")]
    public class TipoServicoController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ITipoServicoRepository _tipoServicoRepository;
        public TipoServicoController(
            IMapper mapper,
            ITipoServicoRepository tipoServicoRepository)
        {
            _mapper = mapper;
            _tipoServicoRepository = tipoServicoRepository;
        }
        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return CustomResponse(_mapper.Map<IEnumerable<TipoServico>, IEnumerable<TipoServicoDto>>(await _tipoServicoRepository.GetAllAsync()));
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            return CustomResponse(_mapper.Map<TipoServico, TipoServicoDto>(await _tipoServicoRepository.GetByIdAsync(id)));
        }

        [HttpPost("new")]
        public async Task<ActionResult> New(TipoServicoDto newTipoServico)
        {
            return CustomResponse(await _tipoServicoRepository.InsertAsync(_mapper.Map<TipoServicoDto, TipoServico>(newTipoServico)));
        }

        [HttpPost("update")]
        public async Task<ActionResult> Update(TipoServicoDto tipoServico)
        {
            await _tipoServicoRepository.Update(_mapper.Map<TipoServicoDto, TipoServico>(tipoServico));

            return Ok(true);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _tipoServicoRepository.DeleteByIdAsync(id);

            return Ok(true);
        }
    }
}
