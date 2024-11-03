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
            };
        }

        public static List<TagDto> TagsToTagDtos(List<Tag> tags)
        {
            return tags.Select(TagToTagDto).ToList();
        }
    }
}
