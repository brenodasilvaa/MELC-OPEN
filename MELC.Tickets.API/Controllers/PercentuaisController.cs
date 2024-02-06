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
    [Route("api/percentuais")]
    public class PercentuaisController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IPercentuaisRepository _percentuaisRepository;
        public PercentuaisController(
            IMapper mapper,
            IPercentuaisRepository PercentuaisRepository)
        {
            _mapper = mapper;
            _percentuaisRepository = PercentuaisRepository;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return CustomResponse(_mapper.Map<IEnumerable<Percentuais>, IEnumerable<PercentuaisDto>>(await _percentuaisRepository.GetAllAsync()));
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            return CustomResponse(_mapper.Map<Percentuais, PercentuaisDto>(await _percentuaisRepository.GetByIdAsync(id)));
        }

        [HttpPost("new")]
        public async Task<ActionResult> New(PercentuaisDto newPercentuais)
        {
            return CustomResponse(await _percentuaisRepository.InsertAsync(_mapper.Map<PercentuaisDto, Percentuais>(newPercentuais)));
        }

        [HttpPost("update")]
        public async Task<ActionResult> Update(PercentuaisDto Percentuais)
        {
            await _percentuaisRepository.Update(_mapper.Map<PercentuaisDto, Percentuais>(Percentuais));

            return Ok(true);
        }
    }
}
