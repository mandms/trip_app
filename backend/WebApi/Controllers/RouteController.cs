using Application.Dto.Pagination;
using Application.Dto.Route;
using Application.Services.RouteService;
using Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [Authorize(Roles = nameof(Roles.Admin))]
        [HttpGet]
        public ActionResult<PaginationResponse<GetAllRoutesDto>> GetAll([FromQuery] FilterParamsWithTag filterParams)
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

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            long userId = (long)HttpContext.Items[ClaimTypes.Sid]!;
            await _service.DeleteRoute(id, userId, cancellationToken);
            return Ok();
        }

        //[Authorize]
        [HttpPost]
        public async Task<ActionResult<CreateRouteDto>> Post([FromBody] CreateRouteDto createRouteDto, CancellationToken cancellationToken)
        {
            // long id = (long)HttpContext.Items[ClaimTypes.Sid]!;
            createRouteDto.UserId = 1;
            await _service.Create(createRouteDto, cancellationToken);
            return Ok();
        }

        [Authorize]
        [HttpPost("{routeId}/tag/{tagId}")]
        public async Task<ActionResult<CreateRouteDto>> AddTag(long routeId, long tagId, CancellationToken cancellationToken)
        {
            long userId = (long)HttpContext.Items[ClaimTypes.Sid]!;
            await _service.AddTag(routeId, userId, tagId, cancellationToken);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{routeId}/tag/{tagId}")]
        public async Task<ActionResult> DeleteTag(long routeId, long tagId, CancellationToken cancellationToken)
        {
            long userId = (long)HttpContext.Items[ClaimTypes.Sid]!;
            await _service.DeleteTag(routeId, userId, tagId, cancellationToken);
            return Ok();
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<RouteDto>> Put(long id, UpdateRouteDto updateRouteDto, CancellationToken cancellationToken)
        {
            long userId = (long)HttpContext.Items[ClaimTypes.Sid]!;
            var route = await _service.UpdateRoute(id, userId, updateRouteDto, cancellationToken);
            return Ok(route);
        }
    }
}
