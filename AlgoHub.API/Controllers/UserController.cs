using AlgoHub.API.Models;
using AlgoHub.API.ViewModels;
using AlgoHub.BLL.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlgoHub.API.Controllers;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost("/register")]
    public async Task<ActionResult<Guid>> Register([FromForm] UserCreateViewModel user)
    {
        var result = await _userService.Register(_mapper.Map<UserCreateModel>(user));
        return result != null ? Ok(result) : BadRequest();
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

        return _mapper.Map<UserViewModel>(user);
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
