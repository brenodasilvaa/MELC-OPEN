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
    [Route("api/frete-desenho")]
    public class FreteDesenhoController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IFreteDesenhoRepository _freteDesenhoRepository;
        public FreteDesenhoController(
            IMapper mapper,
            IFreteDesenhoRepository FreteDesenhoRepository)
        {
            _mapper = mapper;
            _freteDesenhoRepository = FreteDesenhoRepository;
        }
        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return CustomResponse(_mapper.Map<IEnumerable<FreteDesenho>, IEnumerable<FreteDesenhoDto>>(await _freteDesenhoRepository.GetAllAsync()));
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            return CustomResponse(_mapper.Map<FreteDesenho, FreteDesenhoDto>(await _freteDesenhoRepository.GetByIdAsync(id)));
        }

        [HttpPost("new")]
        public async Task<ActionResult> New(FreteDesenhoDto newFreteDesenho)
        {
            return CustomResponse(await _freteDesenhoRepository.InsertAsync(_mapper.Map<FreteDesenhoDto, FreteDesenho>(newFreteDesenho)));
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _freteDesenhoRepository.DeleteByIdAsync(id);

            return Ok(true);
        }

        [HttpPost("update")]
        public async Task<ActionResult> Update(FreteDesenhoDto FreteDesenho)
        {
            await _freteDesenhoRepository.Update(_mapper.Map<FreteDesenhoDto, FreteDesenho>(FreteDesenho));

            return Ok(true);
        }
    }
}
