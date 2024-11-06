using Application.Dto.Location;
using Application.Services.LocationService;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateLocationDto>> Put(long id, UpdateLocationDto updatelocationDto, CancellationToken cancellationToken)
        {
            await _service.Put(id, updatelocationDto, cancellationToken);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _service.Delete(id, cancellationToken);
            return Ok();
        }
    }
}