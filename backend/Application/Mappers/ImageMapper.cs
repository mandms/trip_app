using Domain.Entities;
using Application.Dto.Image;

namespace Application.Mappers
{
    public class ImageMapper
    {
        public static List<ImageDto> ImageLocationToString(List<ImageLocation> images)
        {
            return images.Select(data => new ImageDto { Id = data.Id, Path = data.Image }).ToList();
        }

        public static List<ImageDto> ImageMomentToString(List<ImageMoment> images)
        {
            return images.Select(data => new ImageDto { Id = data.Id, Path = data.Image }).ToList();
        }
    }
}
