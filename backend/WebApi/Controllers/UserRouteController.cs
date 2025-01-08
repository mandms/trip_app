using Microsoft.AspNetCore.Mvc;
using Application.Dto.Pagination;
using Domain.Filters;
using Application.Services.UserRouteService;
using Application.Dto.UserRoute;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRouteController : ControllerBase
    {
        private readonly IUserRouteService _service;
        public UserRouteController(IUserRouteService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<PaginationResponse<UserRouteDto>> GetAll([FromQuery] FilterParams filterParams)
        {
            long userId = (long)HttpContext.Items[ClaimTypes.Sid]!;
            var pagedResponse = _service.GetRoutesByUserId(userId, filterParams);
            return Ok(pagedResponse);
        }

        [HttpDelete("route/{routeId}")]
        public async Task<ActionResult> Delete(long routeId, CancellationToken cancellationToken)
        {
            long userId = (long)HttpContext.Items[ClaimTypes.Sid]!;
            await _service.Delete(userId, routeId, cancellationToken);
            return Ok();
        }

        [HttpPost("route/{routeId}")]
        public async Task<ActionResult> Post(long routeId, [FromBody] CreateUserRouteDto createUserRouteDto, CancellationToken cancellationToken)
        {
            long userId = (long)HttpContext.Items[ClaimTypes.Sid]!;
            await _service.Create(userId, routeId, createUserRouteDto, cancellationToken);
            return Ok();
        }

        [HttpPut("route/{routeId}")]
        public async Task<ActionResult> Put(long routeId, CreateUserRouteDto createUserRouteDto, CancellationToken cancellationToken)
        {
            long userId = (long)HttpContext.Items[ClaimTypes.Sid]!;
            await _service.Put(userId, routeId, createUserRouteDto, cancellationToken);
            return Ok();
        }
    }
}
