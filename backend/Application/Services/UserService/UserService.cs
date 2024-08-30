using Domain.Entities;
using Application.Mappers;
using Domain.Contracts.Repositories;
using Application.Dto.User;

namespace Application.Services.UserService
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository) 
        {
            _repository = repository;
        }

        public async Task Create(CreateUserDto createUserDto, CancellationToken cancellationToken)
        {
            User user = UserMapper.CreateUserDtoUser(createUserDto);
            user.Username = Guid.NewGuid().ToString().Substring(0, 15);
            user.Name = Guid.NewGuid().ToString().Substring(0, 15);

            await _repository.Add(user, cancellationToken);
        }

        public async Task<CurrentUserDto?> GetUser(long id)
        {
            var user = await _repository.GetCurrentUser(id);
            if (user == null)
            {
                return null;
            }
            CurrentUserDto userDto = UserMapper.UserCurrentUser(user);
            return userDto;
        }

        public async Task Put(long id, UpdateUserDto updateUserDto, CancellationToken cancellationToken)
        {
            var user = await _repository.GetById(id);
            if (user == null)
            {
                return;
            }
            UserMapper.UpdateUserDtoUser(user, updateUserDto);
            await _repository.Update(user, cancellationToken);
        }
    }
}
