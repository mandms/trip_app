using NetTopologySuite.Geometries;

namespace Application.Dto.Location
{
    public class LocationDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Point Coordinates { get; set; }
        public int Order { get; set; }
        public List<string> Images { get; set; }
    }
}
