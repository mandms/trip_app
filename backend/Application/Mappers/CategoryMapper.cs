using Application.Dto.Category;
using Domain.Entities;

namespace Application.Mappers
{
    public class CategoryMapper
    {
        public static CategoryDto CategoryToCategoryDto(Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
            };
        }
        public static Category ToCategory(CreateCategoryDto CreateCategoryDto)
        {
            return new Category
            {
                Name = CreateCategoryDto.Name
            };
        }

        public static void UpdateCategory(CreateCategoryDto updateCategoryDto, Category category)
        {
            category.Name = updateCategoryDto.Name;
        }
    }
}
