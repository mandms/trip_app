using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Location
{
    [DisplayName("Coordinates")]
    public class Coordinates
    {
        [Required]
        [MaxLength(90)]
        [MinLength(-90)]
        public double Latitude { get; set; }

        [Required]
        [MaxLength(180)]
        [MinLength(-180)]
        public double Longitude { get; set; }
    }
}
