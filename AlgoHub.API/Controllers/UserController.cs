using AlgoHub.API.Models;
using AlgoHub.API.ViewModels;
using AlgoHub.BLL.Interfaces;
using AlgoHub.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AlgoHub.API.Controllers;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("/register")]
    public async Task<ActionResult<User>> Register(UserCreateViewModel user)
    {
        var userRes = await _userService.Register(new()
        {
            UserName = user.UserName,
            FullName = user.FullName,
            Email = user.Email,
            Password = user.Password,
            IconName = user.IconName,
        });
        return user == null ? CreatedAtAction(nameof(Register), user) : BadRequest();
    }

    [HttpPost("/login")]
    public async Task<ActionResult<UserTokenData>> Login(UserLoginViewModel user)
    {
        UserTokenData? token = await _userService.Login(new()
        {
            UserName = user.UserName,
            Password = user.Password
        });

        return token != null ? Ok(token) : Unauthorized();
    }

    [HttpPost("/refresh")]
    public async Task<ActionResult<UserTokenData>> RefreshToken(UserRefreshTokenViewModel tokens)
    {
        var newTokens = await _userService.RefreshToken(new() { Token = tokens.OldJwtToken, RefreshToken = tokens.RefreshToken });

        return newTokens != null ? Ok(newTokens) : Unauthorized();
    }
}
