using Application.Dto.User;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Application.Services.UserService;
using Application.Mappers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<CreateUserDto>> Post([FromBody] CreateUserDto createUserDto, CancellationToken cancellationToken)
        {
            await _service.Create(createUserDto, cancellationToken);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CurrentUserDto>> GetUser(long id)
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
