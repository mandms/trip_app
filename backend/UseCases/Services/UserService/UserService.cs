using Domain.Entities;
using UseCases.DTOs;
using UseCases.Mappers;
using Domain.Contracts.Repositories;

namespace UseCases.Services.UserService
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository) 
        {
            _repository = repository;
        }

        public User GetById(long id)
        {
            return _repository.GetById(id);
        }

        public void Create(CreateUserDto createUserDto, CancellationToken cancellationToken)
        {
            User user = UserMapper.CreateUserDtoUser(createUserDto);
            user.Username = Guid.NewGuid().ToString().Substring(0, 15);
            user.Name = Guid.NewGuid().ToString().Substring(0, 15);

            _repository.Add(user);
        }
    }
}
