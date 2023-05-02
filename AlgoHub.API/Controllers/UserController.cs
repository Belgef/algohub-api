using AlgoHub.API.Mappings;
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
    public async Task<ActionResult<UserViewModel>> Register([FromForm] UserCreateViewModel user)
    {
        var userRes = await _userService.Register(user.ToUserCreateModel()!);
        return userRes != null ? Ok(userRes.ToUserViewModel()) : BadRequest();
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

    [HttpGet()]
    public async Task<UserViewModel?> GetUserById(Guid userId)
    {
        var user = await _userService.GetUserById(userId);

        return user.ToUserViewModel();
    }

    [HttpGet("/checkUserName")]
    public Task<bool> CheckUserName(string userName)
    {
        return _userService.CheckUserName(userName);
    }

    [HttpGet("/checkEmail")]
    public Task<bool> CheckEmail(string email)
    {
        return _userService.CheckEmail(email);
    }
}
