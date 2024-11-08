using Domain.Entities;

namespace Application.Mappers
{
    public class ImageMapper
    {
        public static List<string> ImageLocationToString(List<ImageLocation> images)
        {
            return images.Select(image => image.Image).ToList();
        }

        public static List<string> ImageMomentToString(List<ImageMoment> images)
        {
            return images.Select(image => image.Image).ToList();
        }
    }
}
