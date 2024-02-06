using AutoMapper;
using MELC.Core.DomainObjects.Dtos;
using MELC.Main.API.Data.Repository;
using MELC.Main.API.Models;
using MELC.WebApi.Core.Identidade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MELC.Main.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/users")]
    public class UserController : BaseController
    {
        private readonly IUserRepository _userRepository;
        public readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost("register")]
        [ClaimsAuthorize("Admin", "Admin")]
        public async Task<IActionResult> Register(UserDto newUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            return CustomResponse(await _userRepository.InsertAsync(_mapper.Map<UserDto, User>(newUser)));
        }

        [HttpPost("update")]
        [ClaimsAuthorize("Admin", "Admin")]
        public async Task<IActionResult> Update(UserDto user)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _userRepository.Update(_mapper.Map<UserDto, User>(user));

            return Ok(true);
        }
    }
}
