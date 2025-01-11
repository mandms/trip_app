using Application.Dto.Route;
using Application.Services.RouteService;
using Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Dto.Pagination;

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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<PaginationResponse<GetAllRoutesDto>> GetAll([FromQuery] FilterParams filterParams)
        {
            var pagedResponse = _service.GetAllRoutes(filterParams); 
            return Ok(pagedResponse); 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RouteDto>> Get(long id)
        {
            var route = await _service.GetRoute(id);
            return new ObjectResult(route);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            await _service.DeleteRoute(id, cancellationToken);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateRouteDto createRouteDto, CancellationToken cancellationToken)
        {
            createRouteDto.UserId = 1;
            await _service.Create(createRouteDto, cancellationToken);
            return Ok();
        }

        [HttpPost("{routeId}/tag/{tagId}")]
        public async Task<ActionResult<CreateRouteDto>> AddTag(long routeId, long tagId, CancellationToken cancellationToken)
        {
            await _service.AddTag(routeId, tagId, cancellationToken);
            return Ok();
        }

        [HttpDelete("{routeId}/tag/{tagId}")]
        public async Task<ActionResult> DeleteTag(long routeId, long tagId, CancellationToken cancellationToken)
        {
            await _service.DeleteTag(routeId, tagId, cancellationToken);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RouteDto>> Put(long id, UpdateRouteDto updateRouteDto, CancellationToken cancellationToken)
        {
            var route = await _service.UpdateRouteModal(id, updateRouteDto, cancellationToken);
            return Ok(route);
        }
    }
}
