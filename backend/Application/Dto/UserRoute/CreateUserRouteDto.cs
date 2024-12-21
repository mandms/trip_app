using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Application.Dto.UserRoute
{
    [DisplayName("Create UserRoute")]
    public class CreateUserRouteDto
    {
        [Required]
        [Range(1, 3)]
        public int State { get; set; }
    }
}
