using Application.Dto.Tag;
using Domain.Entities;

namespace Application.Mappers
{
    public static class TagMapper
    {
        public static TagDto TagToTagDto(Tag tag)
        {
            return new TagDto
            {
                Id = tag.Id,
                Name = tag.Name,
                category = CategoryMapper.CategoryToCategoryDto(tag.Category)
            };
        }

        public static List<TagDto> TagsToTagDtos(List<Tag> tags)
        {
            return tags.Select(TagToTagDto).ToList();
        }

        public static RouteTagDto TagToTagRouteDto(Tag tag)
        {
            return new RouteTagDto
            {
                Id = tag.Id,
                Name = tag.Name
            };
        }

        public static List<RouteTagDto> TagsToTagRouteDtos(List<Tag> tags)
        {
            return tags.Select(TagToTagRouteDto).ToList();
        }

        public static Tag ToTag(CreateTagDto createTagDto, long categoryId)
        {
            return new Tag
            {
                Name = createTagDto.Name,
                CategoryId = categoryId
            };
        }

        public static void UpdateTag(UpdateTagDto updateTagDto, Tag tag)
        {
            tag.Name = updateTagDto.Name;
            tag.CategoryId = updateTagDto.CategoryId;
        }
    }
}
