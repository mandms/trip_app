using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Location
{
    [DisplayName("Location")]
    public class LocationDto
    {
        [Required]
        [MinLength(1)]
        public long Id { get; set; }        
        public string Name { get; set; } = null!;
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }
        public Coordinates Coordinates { get; set; } = null!;
        public int Order { get; set; }
        public List<string> Images { get; set; } = new();
    }
}
