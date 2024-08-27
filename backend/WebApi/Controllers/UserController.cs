using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using UseCases.DTOs;
using UseCases.Services.UserService;

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
            _service.Create(createUserDto, cancellationToken);
            return Ok();
        }
    }
}
