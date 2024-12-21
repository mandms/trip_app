using Application.Dto.Category;
using Application.Dto.Moment;
using Application.Dto.Pagination;
using Application.Mappers;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Filters;

namespace Application.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(
            ICategoryRepository repository)
        {
            _repository = repository;
        }
        public async Task Create(CreateCategoryDto createCategoryDto, CancellationToken cancellationToken)
        {
            Category category = CategoryMapper.ToCategory(createCategoryDto);
            await _repository.Add(category, cancellationToken);
        }

        public async Task Delete(long id, CancellationToken cancellationToken)
        {
            var category = await _repository.GetById(id);
            if (category == null)
            {
                throw new EntityNotFoundException("Category", id);
            }

            await _repository.Remove(category, cancellationToken);
        }

        public PaginationResponse<CategoryDto> GetAllCategories(FilterParams filterParams)
        {
            var categories = _repository.GetAll(filterParams);

            var categoryDtos = categories.Select(category => CategoryMapper.CategoryToCategoryDto(category));

            var pagedResponse = new PaginationResponse<CategoryDto>(categoryDtos, filterParams.PageNumber, filterParams.PageSize);

            return pagedResponse;
        }

        public async Task Put(long id, CreateCategoryDto updateCategoryDto, CancellationToken cancellationToken)
        {
            Category? foundCategory = await _repository.GetById(id);
            if (foundCategory == null)
            {
                throw new EntityNotFoundException("Category", id);
            }

            CategoryMapper.UpdateCategory(updateCategoryDto, foundCategory);

            Category category = await _repository.Update(foundCategory, cancellationToken);
        }
    }
}
