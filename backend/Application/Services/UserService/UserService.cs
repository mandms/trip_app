using Domain.Entities;
using Domain.Exceptions;
using Application.Mappers;
using Domain.Contracts.Repositories;
using Application.Dto.User;
using Domain.Contracts.Utils;

namespace Application.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private const string UsernamePrefix = "user";

        public UserService(
            IUserRepository repository, 
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider) 
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task Create(CreateUserDto createUserDto, CancellationToken cancellationToken)
        {
            User user = UserMapper.CreateUserDtoUser(createUserDto);
            User? foundUser = await _repository.GetUserByEmail(createUserDto.Email, cancellationToken);

            if (foundUser != null)
            {
                throw new Exception("User already exist");
            }

            user.Username = user.Email.Substring(0, 14);
            user.Password = _passwordHasher.Hash(user.Password);
            await _repository.Add(user, cancellationToken);
        }

        public async Task<UserDto?> GetUser(long id)
        {
            var user = await _repository.GetCurrentUser(id);
            if (user == null)
            {
                throw new UserNotFoundException(id);
            }
            UserDto userDto = UserMapper.UserCurrentUser(user);
            return userDto;
        }

        public async Task Put(long id, UpdateUserDto updateUserDto, CancellationToken cancellationToken)
        {
            var user = await _repository.GetById(id);
            if (user == null)
            {
                throw new UserNotFoundException(id);
            }
            UserMapper.UpdateUserDtoUser(user, updateUserDto);
            await _repository.Update(user, cancellationToken);
        }

        public async Task<string> Login(CreateUserDto createUserDto, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserByEmail(createUserDto.Email, cancellationToken);

            if (user == null)
            {
                throw new UserNotFoundException(0);
            }

            var result = _passwordHasher.Verify(createUserDto.Password, user.Password);

            if (!result)
            {
                throw new UserNotFoundException(0);
            }

            string jwt = _jwtProvider.GenerateToken(user);

            return jwt;
        }
    }
}
