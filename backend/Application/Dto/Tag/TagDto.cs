using System.ComponentModel;

namespace Application.Dto.Tag
{
    [DisplayName("Tag")]
    public class TagDto
    {
        public string Name { get; set; }
        public long Id { get; set; }
    }
}
