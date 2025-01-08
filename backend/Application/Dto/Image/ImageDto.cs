using System.ComponentModel;

namespace Application.Dto.Image
{
    [DisplayName("Image")]
    public class ImageDto
    {
        public long Id { get; set; }
        public string Path { get; set; } = string.Empty;
    }
}
