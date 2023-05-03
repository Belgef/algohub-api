using AlgoHub.API.ViewModels;
using AlgoHub.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AlgoHub.API.Controllers;

[ApiController]
[Route("[controller]/")]
public class ProblemController : ControllerBase
{
    private IProblemService _problemService;

    public ProblemController(IProblemService problemService)
    {
        _problemService = problemService;
    }

    [HttpGet("{problemId}")]
    public async Task<ActionResult<ProblemDetailedViewModel>> GetAll(int problemId)
    {
        var problem = await _problemService.GetProblemById(problemId);
        return problem == null ? NotFound() : Ok(problem);
    }

    [HttpGet]
    public Task<IActionResult> SampleUser()
    {
        return Task.FromResult<IActionResult>(Ok(User.FindFirstValue(ClaimTypes.Role)));
    }
}