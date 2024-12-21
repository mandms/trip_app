using Application.Dto.Pagination;
using Application.Dto.Category;
using Domain.Filters;

namespace Application.Services.CategoryService
{
    public interface ICategoryService
    {
        Task Create(CreateCategoryDto createCategoryDto, CancellationToken cancellationToken);
        Task Put(long id, CreateCategoryDto updateCategoryDto, CancellationToken cancellationToken);
        Task Delete(long id, CancellationToken cancellationToken);
        PaginationResponse<CategoryDto> GetAllCategories(FilterParams filterParams);
    }
}
