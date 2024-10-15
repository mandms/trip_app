using Application.Dto.Route;
using Application.Services.RouteService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRouteService _service;
        public RouteController(IRouteService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RouteDto>> Get(int id)
        {
            var route = await _service.GetRoute(id);
            if (route == null)
            {
                return NotFound();
            }
            return new ObjectResult(route);
        }
    }
}
