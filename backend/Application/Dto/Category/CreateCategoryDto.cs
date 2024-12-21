using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Category
{
    [DisplayName("Create category")]
    public class CreateCategoryDto
    {
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string Name { get; set; } = null!;
    }
}
