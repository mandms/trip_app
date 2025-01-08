using Application.Dto.Pagination;
using Application.Dto.Tag;
using Application.Services.TagService;
using Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _service;
        public TagController(ITagService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<PaginationResponse<TagDto>> GetAll([FromQuery] FilterParams filterParams)
        {
            var pagedResponse = _service.GetAllTags(filterParams);
            return Ok(pagedResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TagDto>> Get(long id)
        {
            var route = await _service.GetTag(id);
            return new ObjectResult(route);
        }

        [Authorize(Roles = nameof(Roles.Admin))]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            await _service.Delete(id, cancellationToken);
            return Ok();
        }

        [Authorize(Roles = nameof(Roles.Admin))]
        [HttpPost("category/{categoryId}")]
        public async Task<ActionResult> Post(long categoryId, [FromBody] CreateTagDto createTagDto, CancellationToken cancellationToken)
        {
            await _service.Create(categoryId, createTagDto, cancellationToken);
            return Ok();
        }

        [Authorize(Roles = nameof(Roles.Admin))]
        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateTagDto>> Put(long id, UpdateTagDto updateTagDto, CancellationToken cancellationToken)
        {
            await _service.Put(id, updateTagDto, cancellationToken);
            return Ok();
        }
    }
}
