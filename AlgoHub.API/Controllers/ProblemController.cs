using AlgoHub.API.Models;
using AlgoHub.API.ViewModels;
using AlgoHub.BLL.Interfaces;
using AlgoHub.BLL.Services;
using AlgoHub.DAL.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace AlgoHub.API.Controllers;

[ApiController]
[Route("[controller]/")]
public class ProblemController : ControllerBase
{
    private readonly IProblemService _problemService;
    private readonly IMapper _mapper;

    public ProblemController(IProblemService problemService, IMapper mapper)
    {
        _problemService = problemService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<ProblemViewModel[]>> GetAll()
    {
        var problems = await _problemService.GetProblems();

        return problems != null ? Ok(_mapper.Map<ProblemViewModel[]>(problems)) : BadRequest();
    }

    [HttpGet("Deleted")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<ProblemViewModel[]>> GetDeleted()
    {
        var problems = await _problemService.GetProblems(true);

        return problems != null ? Ok(_mapper.Map<ProblemViewModel[]>(problems)) : BadRequest();
    }

    [HttpGet("{problemId}")]
    public async Task<ActionResult<ProblemViewModel>> Get(int problemId)
    {
        var problem = await _problemService.GetProblemById(problemId);

        return problem != null && (!(problem.Deleted ?? false) || User.IsInRole("Administrator")) ? Ok(problem) : NotFound();
    }

    [HttpPost]
    [Authorize(Roles = "User,Administrator")]
    public async Task<ActionResult<int?>> AddProblem([FromForm] ProblemCreateViewModel problem)
    {
        string? userId = User.FindFirstValue("Id");

        if (userId == null)
        {
            return Unauthorized();
        }

        ProblemCreateModel model = _mapper.Map<ProblemCreateModel>(problem);
        model.AuthorId = Guid.Parse(userId!);
        model.Tests = JsonSerializer.Deserialize<TestModel[]>(problem.TestsString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        model.Tags = JsonSerializer.Deserialize<string[]>(problem.TagsString);

        int? result = await _problemService.AddProblem(model);

        return result != null ? Ok(result) : BadRequest();
    }

    [HttpPost("Delete/{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<bool>> DeleteProblem(int id)
    {
        bool result = await _problemService.DeleteProblem(id);

        return result ? Ok() : BadRequest();
    }

    [HttpPost("Retrieve/{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<bool>> RetrieveProblem(int id)
    {
        bool result = await _problemService.RetrieveProblem(id);

        return result ? Ok() : BadRequest();
    }
}
