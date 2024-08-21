using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Foundation.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IBaseRepository<User> _userRepository;
        public UserController(IBaseRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            User user = await _userRepository.GetById(id);
            return new ObjectResult(user);
        }

        [HttpGet]
        public ActionResult<IQueryable<User>> GetUsers()
        {
            IQueryable<User> users = _userRepository.GetAll();
            return new ObjectResult(users);
        }
    }
}
