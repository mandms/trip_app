using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Tag
{
    [DisplayName("Route tag")]
    public class RouteTagDto
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
    }
}
