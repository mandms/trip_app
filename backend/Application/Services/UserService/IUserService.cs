using Application.Dto.User;
using Domain.Entities;

namespace Application.Services.UserService
{
    public interface IUserService
    {
        Task Create(CreateUserDto createUserDto, CancellationToken cancellationToken);
        Task<CurrentUserDto?> GetUser(long id);
        Task Put(long id, UpdateUserDto updateUserDto, CancellationToken cancellationToken);
    }
}
