using Domain.Entities;
using Domain.Exceptions;
using Application.Mappers;
using Domain.Contracts.Repositories;
using Application.Dto.User;
using Domain.Contracts.Utils;
using Application.Services.FileService;

namespace Application.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly IRoleRepository _roleRepository;
        private readonly IFileService _fileService;
        private readonly IDbTransaction _dbTransaction;

        public UserService(
            IUserRepository repository, 
            IRoleRepository roleRepository,
            IPasswordHasher passwordHasher,
            IFileService fileService,
            IDbTransaction transaction,
            IJwtProvider jwtProvider) 
        {
            _repository = repository;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _dbTransaction = transaction;
            _jwtProvider = jwtProvider;
            _fileService = fileService;
        }

        public async Task Create(CreateUserDto createUserDto, CancellationToken cancellationToken)
        {
            User user = UserMapper.CreateUserDtoUser(createUserDto);
            User? foundUser = await _repository.GetUserByEmail(createUserDto.Email, cancellationToken);

            if (foundUser != null)
            {
                throw new Exception("User already exist");
            }

            var role = await _roleRepository.GetRoleByName("User");

            if (role == null)
            {
                throw new Exception("Role not found");
            }

            user.Roles.Add(role);
            user.Username = createUserDto.Username;
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
            var updateUser = () => UpdateUser(user, updateUserDto, cancellationToken);
            await _dbTransaction.Transaction(updateUser);
        }

        private async Task UpdateUser(User user, UpdateUserDto updateUserDto, CancellationToken cancellationToken)
        {
            if (updateUserDto.Avatar != null)
            {
                updateUserDto.Avatar.Path = await _fileService.SaveFileAsync(updateUserDto.Avatar.Image, cancellationToken);
            }
            UserMapper.UpdateUserDtoUser(user, updateUserDto);
            await _repository.Update(user, cancellationToken);
        }

        public async Task<string> Login(LoginUserDto loginUserDto, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserByEmail(loginUserDto.Email, cancellationToken);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var result = _passwordHasher.Verify(loginUserDto.Password, user.Password);

            if (!result)
            {
                throw new UserNotFoundException();
            }

            string jwt = _jwtProvider.GenerateToken(user);

            return jwt;
        }
    }
}
