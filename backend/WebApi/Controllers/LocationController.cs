using Application.Dto.Location;
using Application.Services.LocationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _service;
        public LocationController(ILocationService service)
        {
            _service = service;
        }

        [HttpPost("route/{routeId}")]
        public async Task<ActionResult<CreateLocationDto>> Post(long routeId, [FromBody] CreateLocationDto createLocationDto, CancellationToken cancellationToken)
        {
            await _service.Create(createLocationDto, routeId, cancellationToken);
            return Ok();
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateLocationDto>> Put(long id, UpdateLocationDto updatelocationDto, CancellationToken cancellationToken)
        {
            long userId = (long)HttpContext.Items[ClaimTypes.Sid]!;
            await _service.Put(id, userId, updatelocationDto, cancellationToken);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            long userId = (long)HttpContext.Items[ClaimTypes.Sid]!;
            await _service.Delete(id, userId, cancellationToken);
            return Ok();
        }
    }
}
