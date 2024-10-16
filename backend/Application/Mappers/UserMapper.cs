using Application.Dto.User;
using Domain.Entities;

namespace Application.Mappers
{
    public static class UserMapper
    {
        public static UserDto UserCurrentUser(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Avatar = user.Avatar,
            };
        }

        public static void UpdateUserDtoUser(User user, UpdateUserDto userDto)
        {
            user.Avatar = userDto.Avatar;
            user.Biography = userDto.Biography;
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

        public static List<UserDto> UsersToUserDtos(List<User> users)
        {
            return users.Select(UserCurrentUser).ToList();
        }

        public static UserRouteDto UserUserRoute(User user)
        {
            return new UserRouteDto
            {
                Id = user.Id,
                Username = user.Username,
                Avatar = user.Avatar,
            };
        }
    }
}
