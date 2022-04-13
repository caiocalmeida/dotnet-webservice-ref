using DotnetWsRef.Api.Middleware;
using DotnetWsRef.Application.User;
using Microsoft.AspNetCore.Mvc;

namespace DotnetWsRef.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize(AuthType.ApiKey)]
    public async Task<IEnumerable<UserDto>> Get()
    {
        return (await _userService.GetUsers()).Select(u => UserDto.From(u));
    }

    [HttpPost]
    [Authorize(AuthType.Forbid)]
    public async Task<UserDto> Post([FromBody] UserDto user)
    {
        return UserDto.From(await _userService.AddUser(user.NewUser()));
    }
}