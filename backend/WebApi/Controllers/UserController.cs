using Application.Dto.User;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Application.Services.UserService;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IBaseRepository<User> _userRepository;
        public UserController(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("/registration")]
        public async Task<ActionResult<CreateUserDto>> Post([FromBody] CreateUserDto createUserDto, CancellationToken cancellationToken)
        {
            await _service.Create(createUserDto, cancellationToken);
            return Ok();
        }

        [HttpPost("/login")]
        public async Task<ActionResult<CreateUserDto>> PostLogin([FromBody] CreateUserDto createUserDto, CancellationToken cancellationToken)
        {
            string token = await _service.Login(createUserDto, cancellationToken);
            return Ok(token);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(long id)
        {
            var user = await _service.GetUser(id);
            return new ObjectResult(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateUserDto>> Put(long id, UpdateUserDto updateUserDto, CancellationToken cancellationToken)
        {
            if (updateUserDto == null)
            {
                return BadRequest();
            }
            await _service.Put(id, updateUserDto, cancellationToken);
            return Ok();
        }

    }
}
