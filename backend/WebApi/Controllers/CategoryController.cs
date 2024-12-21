using Application.Dto.Pagination;
using Application.Dto.Category;
using Domain.Filters;
using Microsoft.AspNetCore.Mvc;
using Application.Services.CategoryService;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<PaginationResponse<CategoryDto>> GetAll([FromQuery] FilterParams filterParams)
        {
            var pagedResponse = _service.GetAllCategories(filterParams);
            return Ok(pagedResponse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            await _service.Delete(id, cancellationToken);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateCategoryDto createCategoryDto, CancellationToken cancellationToken)
        {
            await _service.Create(createCategoryDto, cancellationToken);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(long id, CreateCategoryDto createCategoryDto, CancellationToken cancellationToken)
        {
            await _service.Put(id, createCategoryDto, cancellationToken);
            return Ok();
        }
    }
}
