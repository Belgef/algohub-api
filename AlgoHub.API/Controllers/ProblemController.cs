using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AlgoHub.API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]/[action]")]
public class ProblemController : ControllerBase
{
    [Authorize(Roles = "Administrator")]
    [HttpGet]
    public Task<IActionResult> SampleAdmin()
    {
        return Task.FromResult<IActionResult>(Ok(User.FindFirstValue(ClaimTypes.Role)));
    }

    [HttpGet]
    public Task<IActionResult> SampleUser()
    {
        return Task.FromResult<IActionResult>(Ok(User.FindFirstValue(ClaimTypes.Role)));
    }
}