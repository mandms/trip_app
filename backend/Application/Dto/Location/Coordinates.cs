using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Location
{
    [DisplayName("Coordinates")]
    public class Coordinates
    {
        [Required]
        [Range(-90, 90)]
        public double Latitude { get; set; }

        [Required]
        [Range(-180, 180)]
        public double Longitude { get; set; }
    }
}
