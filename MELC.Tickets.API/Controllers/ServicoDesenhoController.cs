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
    [Route("api/servico-desenho")]
    public class ServicoDesenhoController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IServicoDesenhoRepository _servicoDesenhoRepository;
        public ServicoDesenhoController(
            IMapper mapper,
            IServicoDesenhoRepository ServicoDesenhoRepository)
        {
            _mapper = mapper;
            _servicoDesenhoRepository = ServicoDesenhoRepository;
        }
        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return CustomResponse(_mapper.Map<IEnumerable<DesenhoServico>, IEnumerable<ServicoDesenhoDto>>(await _servicoDesenhoRepository.GetAllAsync()));
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            return CustomResponse(_mapper.Map<DesenhoServico, ServicoDesenhoDto>(await _servicoDesenhoRepository.GetByIdAsync(id)));
        }

        [HttpPost("new")]
        public async Task<ActionResult> New(ServicoDesenhoDto newServicoDesenho)
        {
            return CustomResponse(await _servicoDesenhoRepository.InsertAsync(_mapper.Map<ServicoDesenhoDto, DesenhoServico>(newServicoDesenho)));
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _servicoDesenhoRepository.DeleteByIdAsync(id);

            return Ok(true);
        }

        [HttpPost("update")]
        public async Task<ActionResult> Update(ServicoDesenhoDto ServicoDesenho)
        {
            await _servicoDesenhoRepository.Update(_mapper.Map<ServicoDesenhoDto, DesenhoServico>(ServicoDesenho));

            return Ok(true);
        }
    }
}
