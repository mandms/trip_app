using Domain.Entities;
using UseCases.DTOs;

namespace UseCases.Mappers
{
    public static class UserMapper
    {

        public static User CreateUserDtoUser(CreateUserDto createUserDto)
        {
            return new User
            {
                Email = createUserDto.Email,
                Password = createUserDto.Password,
            };
        }
    }
}
