using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Tag
{
    [DisplayName("Update tag")]
    public class UpdateTagDto
    {
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string Name { get; set; } = null!;
        [Required]
        [Range(1, System.Int32.MaxValue)]
        public long CategoryId { get; set; }
    }
}
