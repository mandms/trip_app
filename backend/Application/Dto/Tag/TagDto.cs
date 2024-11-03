using System.ComponentModel;

namespace Application.Dto.Tag
{
    [DisplayName("Tag")]
    public class TagDto
    {
        public string Name { get; set; } = null!;
        public long Id { get; set; }
    }
}
