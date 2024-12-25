using Application.Dto.Pagination;
using Application.Dto.User;
using Application.Mappers;
using Application.Services.FileService;
using Domain.Contracts.Repositories;
using Domain.Contracts.Utils;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Filters;

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
                throw new UserAlreadyExistsException();
            }

            var role = await _roleRepository.GetRoleByName("User");

            if (role == null)
            {
                throw new EntityNotFoundException("Role");
            }

            user.Roles.Add(role);
            user.Username = createUserDto.Username;
            user.Password = _passwordHasher.Hash(user.Password);
            await _repository.Add(user, cancellationToken);
        }

        public PaginationResponse<GetAllUsersDto> GetAllUsers(FilterParams filterParams)
        {
            var users = _repository.GetAllUsers(filterParams);

            var usersDto = users.Select(user => UserMapper.UserGetAllUsersDto(user));

            var pagedResponse = new PaginationResponse<GetAllUsersDto>(usersDto, filterParams.PageNumber, filterParams.PageSize);

            return pagedResponse;
        }

        public async Task<UserDto?> GetUser(long id)
        {
            var user = await _repository.GetCurrentUser(id);
            if (user == null)
            {
                throw new EntityNotFoundException("User", id);
            }
            UserDto userDto = UserMapper.UserUserDto(user);
            return userDto;
        }

        public async Task Put(long id, UpdateUserDto updateUserDto, CancellationToken cancellationToken)
        {
            var user = await _repository.GetById(id);
            if (user == null)
            {
                throw new EntityNotFoundException("User", id);
            }
            var updateUser = () => UpdateUser(user, updateUserDto, cancellationToken);
            await _dbTransaction.Transaction(updateUser);
        }

        private async Task UpdateUser(User user, UpdateUserDto updateUserDto, CancellationToken cancellationToken)
        {
            if (updateUserDto.Avatar != null)
            {
                if (user.Avatar != null)
                {
                    _fileService.DeleteFile(user.Avatar);
                }
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
                throw new EntityNotFoundException("User");
            }

            var result = _passwordHasher.Verify(loginUserDto.Password, user.Password);

            if (!result)
            {
                throw new InvalidPasswordException();
            }

            string jwt = _jwtProvider.GenerateToken(user);

            return jwt;
        }

        public async Task DeleteAvatar(long userId, CancellationToken cancellationToken)
        {
            var user = await _repository.GetById(userId);
            if (user == null)
            {
                throw new EntityNotFoundException("User", userId);
            }
            _fileService.DeleteFile(user.Avatar);

            user.Avatar = "user_default.png";

            await _repository.Update(user, cancellationToken);
        }
    }
}
