using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Image
{
    [DisplayName("Create images")]
    public class CreateImagesDto
    {
        [Required]
        public List<CreateImageDto> Images { get; set; } = new();
    }
}
