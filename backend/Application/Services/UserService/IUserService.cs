using Application.Dto.User;

namespace Application.Services.UserService
{
    public interface IUserService
    {
        Task Create(CreateUserDto createUserDto, CancellationToken cancellationToken);
        Task<UserDto?> GetUser(long id);
        Task Put(long id, UpdateUserDto updateUserDto, CancellationToken cancellationToken);
        public Task<string> LoginPage(LoginUserDto loginUserDto, CancellationToken cancellationToken);
    }
}
