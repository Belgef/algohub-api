using AlgoHub.API.Models;
using AlgoHub.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AlgoHub.API.Controllers;

[ApiController]
[Route("[controller]/")]
public class VoteController : ControllerBase
{
    private readonly IVoteService _voteService;

    public VoteController(IVoteService voteService)
    {
        _voteService = voteService;
    }

    [HttpPost("Lesson")]
    [Authorize(Roles = "User,Administrator")]
    public async Task<ActionResult<int?>> AddLessonVote([FromForm] VoteViewModel vote)
    {
        string? userId = User.FindFirstValue("Id");

        if (userId == null)
        {
            return Unauthorized();
        }

        int? result = await _voteService.AddLessonVote(vote.Id, Guid.Parse(userId!), vote.IsUpvote);

        return result != null ? Ok(result) : BadRequest();
    }

    [HttpGet("Lesson/{lessonId}")]
    [Authorize(Roles = "User,Administrator")]
    public async Task<ActionResult<bool?>> GetLessonVote(int lessonId)
    {
        string? userId = User.FindFirstValue("Id");

        if (userId == null)
        {
            return Unauthorized();
        }

        bool? result = await _voteService.GetLessonVote(lessonId, Guid.Parse(userId!));

        return Ok(result);
    }

    [HttpPost("Problem")]
    [Authorize(Roles = "User,Administrator")]
    public async Task<ActionResult<int?>> AddProblemVote([FromForm] VoteViewModel vote)
    {
        string? userId = User.FindFirstValue("Id");

        if (userId == null)
        {
            return Unauthorized();
        }

        int? result = await _voteService.AddProblemVote(vote.Id, Guid.Parse(userId!), vote.IsUpvote);

        return result != null ? Ok(result) : BadRequest();
    }

    [HttpGet("Problem/{problemId}")]
    [Authorize(Roles = "User,Administrator")]
    public async Task<ActionResult<bool?>> GetProblemVote(int problemId)
    {
        string? userId = User.FindFirstValue("Id");

        if (userId == null)
        {
            return Unauthorized();
        }

        bool? result = await _voteService.GetProblemVote(problemId, Guid.Parse(userId!));

        return Ok(result);
    }

    [HttpPost("Solve")]
    [Authorize(Roles = "User,Administrator")]
    public async Task<ActionResult<int?>> AddSolveVote([FromForm] VoteViewModel vote)
    {
        string? userId = User.FindFirstValue("Id");

        if (userId == null)
        {
            return Unauthorized();
        }

        int? result = await _voteService.AddSolveVote(vote.Id, Guid.Parse(userId!), vote.IsUpvote);

        return result != null ? Ok(result) : BadRequest();
    }

    [HttpGet("Solve/{solveId}")]
    [Authorize(Roles = "User,Administrator")]
    public async Task<ActionResult<bool?>> GetSolveVote(int solveId)
    {
        string? userId = User.FindFirstValue("Id");

        if (userId == null)
        {
            return Unauthorized();
        }

        bool? result = await _voteService.GetSolveVote(solveId, Guid.Parse(userId!));

        return Ok(result);
    }
}