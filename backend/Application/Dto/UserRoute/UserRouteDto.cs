using Application.Dto.Route;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.UserRoute
{
    [DisplayName("UserRoute")]
    public class UserRouteDto
    {
        [Required]
        [Range(1, 3)]
        public int State { get; set; }
        [Required]
        public GetAllRoutesDto Route { get; set; } = null!;
    }
}
