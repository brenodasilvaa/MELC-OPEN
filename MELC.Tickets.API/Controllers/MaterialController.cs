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
    [Route("api/material")]
    public class MaterialController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMaterialRepository _materialRepository;
        public MaterialController(
            IMapper mapper,
            IMaterialRepository MaterialRepository)
        {
            _mapper = mapper;
            _materialRepository = MaterialRepository;
        }
        [HttpGet("get-all")]
        public async Task<ActionResult> GetAll()
        {
            return CustomResponse(_mapper.Map<IEnumerable<Material>, IEnumerable<MaterialDto>>(await _materialRepository.GetAllAsync()));
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            return CustomResponse(_mapper.Map<Material, MaterialDto>(await _materialRepository.GetByIdAsync(id)));
        }

        [HttpPost("new")]
        public async Task<ActionResult> New(MaterialDto newMaterial)
        {
            return CustomResponse(await _materialRepository.InsertAsync(_mapper.Map<MaterialDto, Material>(newMaterial)));
        }

        [HttpPost("update")]
        public async Task<ActionResult> Update(MaterialDto Material)
        {
            await _materialRepository.Update(_mapper.Map<MaterialDto, Material>(Material));

            return Ok(true);
        }
    }
}
