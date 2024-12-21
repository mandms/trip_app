using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Category
{
    [DisplayName("Category")]
    public class CategoryDto
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
    }
}
