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
    [Route("api/material-desenho")]
    public class MaterialDesenhoController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMaterialDesenhoRepository _MaterialDesenhoRepository;
        public MaterialDesenhoController(
            IMapper mapper,
            IMaterialDesenhoRepository MaterialDesenhoRepository)
        {
            _mapper = mapper;
            _MaterialDesenhoRepository = MaterialDesenhoRepository;
        }
        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return CustomResponse(_mapper.Map<IEnumerable<MaterialDesenho>, IEnumerable<MaterialDesenhoDto>>(await _MaterialDesenhoRepository.GetAllAsync()));
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            return CustomResponse(_mapper.Map<MaterialDesenho, MaterialDesenhoDto>(await _MaterialDesenhoRepository.GetByIdAsync(id)));
        }

        [HttpPost("new")]
        public async Task<ActionResult> New(MaterialDesenhoDto newMaterialDesenho)
        {
            return CustomResponse(await _MaterialDesenhoRepository.InsertAsync(_mapper.Map<MaterialDesenhoDto, MaterialDesenho>(newMaterialDesenho)));
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _MaterialDesenhoRepository.DeleteByIdAsync(id);

            return Ok(true);
        }

        [HttpPost("update")]
        public async Task<ActionResult> Update(MaterialDesenhoDto MaterialDesenho)
        {
            await _MaterialDesenhoRepository.Update(_mapper.Map<MaterialDesenhoDto, MaterialDesenho>(MaterialDesenho));

            return Ok(true);
        }
    }
}
