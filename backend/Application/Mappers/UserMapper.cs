using Application.Dto.User;
using Domain.Entities;

namespace Application.Mappers
{
    public static class UserMapper
    {
        public static UserDto UserUserDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Avatar = user.Avatar,
                Biography = user.Biography
            };
        }

        public static GetAllUsersDto UserGetAllUsersDto(User user)
        {
            return new GetAllUsersDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Avatar = user.Avatar,
                Roles = user.Roles.Select(r => r.Name).ToList(),
                Biography = user.Biography
            };
        }

        public static void UpdateUserDtoUser(User user, UpdateUserDto userDto)
        {
            if (userDto.Avatar != null)
            {
                user.Avatar = userDto.Avatar.Path;
            }
            user.Biography = userDto.Biography ?? user.Biography;
            user.Username = userDto.Username;
        }

        public static User CreateUserDtoUser(CreateUserDto createUserDto)
        {
            return new User
            {
                Email = createUserDto.Email,
                Password = createUserDto.Password,
            };
        }

        public static AuthorUserDto UserAuthor(User user)
        {
            return new AuthorUserDto
            {
                Id = user.Id,
                Username = user.Username,
                Avatar = user.Avatar,
            };
        }
    }
}
