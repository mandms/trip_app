﻿using Application.Dto.Pagination;
using Application.Dto.User;
using Domain.Filters;

namespace Application.Services.UserService
{
    public interface IUserService
    {
        Task Create(CreateUserDto createUserDto, CancellationToken cancellationToken);
        PaginationResponse<GetAllUsersDto> GetAllUsers(FilterParams filterParams);
        Task<UserDto?> GetUser(long id);
        Task Put(long id, UpdateUserDto updateUserDto, CancellationToken cancellationToken);
        public Task<string> Login(LoginUserDto loginUserDto, CancellationToken cancellationToken);
        public Task DeleteAvatar(long userId, CancellationToken cancellationToken);
        Task Delete(long id, CancellationToken cancellationToken);
    }
}
