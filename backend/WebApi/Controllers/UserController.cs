﻿using Application.Dto.Pagination;
using Application.Dto.User;
using Application.Services.UserService;
using Domain.Entities;
using Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Attributes;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("registration")]
        public async Task<ActionResult<CreateUserDto>> Post([FromBody] CreateUserDto createUserDto, CancellationToken cancellationToken)
        {
            await _service.Create(createUserDto, cancellationToken);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<CreateUserDto>> PostLogin([FromBody] LoginUserDto loginUserDto, CancellationToken cancellationToken)
        {
            string token = await _service.Login(loginUserDto, cancellationToken);
            return Ok(token);
        }

        [Authorize(Roles = nameof(Roles.Admin))]
        [HttpGet]
        public ActionResult<PaginationResponse<GetAllUsersDto>> GetAll([FromQuery] FilterParams filterParams)
        {
            var pagedResponse = _service.GetAllUsers(filterParams);
            return Ok(pagedResponse);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<UserDto>> GetMe()
        {
            long id = (long)HttpContext.Items[ClaimTypes.Sid]!;
            var user = await _service.GetUser(id);
            return new ObjectResult(user);
        }

        [HttpPut("{id}")]
        [AuthorizeOwnerOrAdmin(typeof(User))]
        public async Task<ActionResult<UpdateUserDto>> Put(long id, UpdateUserDto updateUserDto, CancellationToken cancellationToken)
        {
            if (updateUserDto == null)
            {
                return BadRequest();
            }
            await _service.Put(id, updateUserDto, cancellationToken);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        [AuthorizeOwnerOrAdmin(typeof(User))]
        public async Task<ActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            await _service.Delete(id, cancellationToken);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}/avatar")]
        [AuthorizeOwnerOrAdmin(typeof(User))]
        public async Task<ActionResult> DeleteAvatar(long id, CancellationToken cancellationToken)
        {
            await _service.DeleteAvatar(id, cancellationToken);
            return Ok();
        }
    }
}
