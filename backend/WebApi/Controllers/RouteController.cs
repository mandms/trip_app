using Application.Dto.Pagination;
using Application.Dto.Route;
using Application.Services.RouteService;
using Domain.Entities;
using Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Attributes;

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

        [HttpGet("published")]
        public ActionResult<PaginationResponse<GetAllRoutesDto>> GetAllPublished([FromQuery] FilterParamsWithTag filterParams)
        {
            var pagedResponse = _service.GetAllPublishedRoutes(filterParams);
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
        [AuthorizeOwnerOrAdmin(typeof(Domain.Entities.Route))]
        public async Task<ActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            await _service.DeleteRoute(id, cancellationToken);
            return Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CreateRouteDto>> Post([FromBody] CreateRouteDto createRouteDto, CancellationToken cancellationToken)
        {
            long id = (long)HttpContext.Items[ClaimTypes.Sid]!;
            createRouteDto.UserId = id;
            await _service.Create(createRouteDto, cancellationToken);
            return Ok();
        }

        [Authorize]
        [HttpPost("{id}/tags")]
        [AuthorizeOwnerOrAdmin(typeof(Domain.Entities.Route))]
        public async Task<ActionResult<CreateRouteDto>> AddTag(long id, List<long> tagIds, CancellationToken cancellationToken)
        {
            await _service.AddTags(id, tagIds, cancellationToken);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}/tags")]
        [AuthorizeOwnerOrAdmin(typeof(Domain.Entities.Route))]
        public async Task<ActionResult> DeleteTag(long id, List<long> tagIds, CancellationToken cancellationToken)
        {
            await _service.DeleteTags(id, tagIds, cancellationToken);
            return Ok();
        }

        [Authorize]
        [HttpPut("{id}")]
        [AuthorizeOwnerOrAdmin(typeof(Domain.Entities.Route))]
        public async Task<ActionResult<RouteDto>> Put(long id, UpdateRouteDto updateRouteDto, CancellationToken cancellationToken)
        {
            var route = await _service.UpdateRoute(id, updateRouteDto, cancellationToken);
            return Ok(route);
        }
    }
}
