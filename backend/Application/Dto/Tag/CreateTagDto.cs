using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Tag
{
    [DisplayName("Create tag")]
    public class CreateTagDto
    {
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string Name { get; set; } = null!;
    }
}
