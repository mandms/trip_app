using Domain.Entities;
using UseCases.DTOs;

namespace UseCases.Services.UserService
{
    public interface IUserService
    {
        User GetById(long id);
        void Create(CreateUserDto createUserDto, CancellationToken cancellationToken);
    }
}
