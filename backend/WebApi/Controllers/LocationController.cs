using Application.Dto.Image;
using Application.Dto.Location;
using Application.Services.LocationService;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Attributes;

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

        [Authorize]
        [HttpPost("route/{routeId}")]
        [AuthorizeOwnerOrAdmin(typeof(Location))]
        public async Task<ActionResult<CreateLocationDto>> Post(long routeId, [FromBody] CreateLocationDto createLocationDto, CancellationToken cancellationToken)
        {
            await _service.Create(createLocationDto, routeId, cancellationToken);
            return Ok();
        }

        [Authorize]
        [HttpPut("{id}")]
        [AuthorizeOwnerOrAdmin(typeof(Location))]
        public async Task<ActionResult<UpdateLocationDto>> Put(long id, UpdateLocationDto updatelocationDto, CancellationToken cancellationToken)
        {
            await _service.Put(id, updatelocationDto, cancellationToken);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        [AuthorizeOwnerOrAdmin(typeof(Location))]
        public async Task<ActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            await _service.Delete(id, cancellationToken);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}/images")]
        [AuthorizeOwnerOrAdmin(typeof(Location))]
        public async Task<ActionResult> DeleteImage(long id, [FromBody] List<long> imagesId, CancellationToken cancellationToken)
        {
            await _service.DeleteImages(id, imagesId, cancellationToken);
            return Ok();
        }

        [HttpPost("{id}/images")]
        [AuthorizeOwnerOrAdmin(typeof(Location))]
        public async Task<ActionResult<CreateLocationDto>> AddImage(long id, [FromBody] List<CreateImageDto> createImagesDto, CancellationToken cancellationToken)
        {
            await _service.AddImages(id, createImagesDto, cancellationToken);
            return Ok();
        }
    }
}
