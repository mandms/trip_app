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
        public ActionResult<IQueryable<GetAllRoutesDto>> Get()
        {
            var routes = _service.GetAllRoutes(); 
            return Ok(routes.ToList()); 
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _service.DeleteRoute(id, cancellationToken);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<CreateRouteDto>> Post([FromBody] CreateRouteDto createRouteDto, CancellationToken cancellationToken)
        {
            await _service.Create(createRouteDto, cancellationToken);
            return Ok();
        }
    }
}
