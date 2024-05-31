using Competitions.Application.Services;
using Competitions.Contracts.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Competitions.Controllers
{
    [ApiController]
    public class UsersController: ControllerBase
    {
        private readonly UsersService _usersService;

        public UsersController(UsersService service)
        {
            _usersService = service;
        }

        [HttpPost("/register")]
        public async Task<IResult> Register([FromBody] RegisterUserRequest request)
        {
            await _usersService.Register(request.Username, request.Email, request.Password);

            return Results.Ok();
        }

        [HttpPost("/login")]
        public async Task<IResult> Login([FromBody] LoginUserRequest request)
        {
            var token = await _usersService.Login(request.Email, request.Password);

            Response.Cookies.Append("JwtToken", token);

            return Results.Ok(token);
        }

        [HttpPost("/logout")]
        [Authorize]
        public async Task<IResult> Logout()
        {
            Response.Cookies.Delete("JwtToken");

            return Results.Ok();
        }
    }
}
