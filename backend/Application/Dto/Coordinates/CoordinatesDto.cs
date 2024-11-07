using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Coordinates
{
    [DisplayName("Coordinates")]
    public class CoordinatesDto
    {
        [Required]
        [Range(-90, 90)]
        public double Latitude { get; set; }

        [Required]
        [Range(-180, 180)]
        public double Longitude { get; set; }

        public static NetTopologySuite.Geometries.Point CreatePoint(CoordinatesDto coordinates)
        {
            return new NetTopologySuite.Geometries.Point(
                new NetTopologySuite.Geometries.Coordinate(
                    coordinates.Longitude,
                    coordinates.Latitude));
        }
    }
}
