namespace Application.Dto.Location
{
    public class CreateLocationDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Coordinates Coordinates { get; set; } = null!;
        public int Order { get; set; } = 0!;
        public List<string> Images { get; set; } = null!;
    }
}
