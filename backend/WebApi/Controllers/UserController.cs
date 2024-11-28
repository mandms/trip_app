using Application.Dto.Pagination;
using Application.Dto.Route;
using Application.Dto.User;
using Application.Services.UserService;
using Domain.Entities;
using Domain.Filters;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("registration")]
        public async Task<ActionResult<CreateUserDto>> Post([FromBody] CreateUserDto createUserDto, CancellationToken cancellationToken)
        {
            await _service.Create(createUserDto, cancellationToken);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<CreateUserDto>> PostLogin([FromBody] LoginUserDto loginUserDto, CancellationToken cancellationToken)
        {
            string token = await _service.Login(loginUserDto, cancellationToken);
            return Ok(token);
        }

        [HttpGet]
        public ActionResult<PaginationResponse<GetAllUsersDto>> GetAll([FromQuery] FilterParams filterParams)
        {
            var pagedResponse = _service.GetAllUsers(filterParams);
            return Ok(pagedResponse);
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
