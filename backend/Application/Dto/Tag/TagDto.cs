using Application.Dto.Category;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Tag
{
    [DisplayName("Tag")]
    public class TagDto
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public CategoryDto category { get; set; } = null!;
    }
}
