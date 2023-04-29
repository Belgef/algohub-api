using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlgoHub.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProblemControler : ControllerBase
{
    [Authorize]
    [HttpGet]
    public Task<IActionResult> Sample()
    {
        return Task.FromResult<IActionResult>(Ok());
    }
}