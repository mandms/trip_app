using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using UseCases.Services.User;

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

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(long id)
        {
            User user = _service.GetById(id);
            return new ObjectResult(user);
        }
    }
}
