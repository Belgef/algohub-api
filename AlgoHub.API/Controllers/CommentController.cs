using AlgoHub.API.Models;
using AlgoHub.API.ViewModels;
using AlgoHub.BLL.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AlgoHub.API.Controllers;

[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;

    public CommentController(ICommentService commentService, IMapper mapper)
    {
        _commentService = commentService;
        _mapper = mapper;
    }

    [HttpGet("/LessonComments")]
    public async Task<ActionResult<LessonCommentViewModel[]>> GetLessonComments([FromQuery] int lessonId)
    {
        var comments = await _commentService.GetLessonComments(lessonId);

        return comments != null ? Ok(_mapper.Map<LessonCommentViewModel[]>(comments)) : BadRequest();
    }

    [HttpGet("/ProblemComments")]
    public async Task<ActionResult<ProblemCommentViewModel[]>> GetProblemComments([FromQuery] int problemId)
    {
        var comments = await _commentService.GetProblemComments(problemId);

        return comments != null ? Ok(_mapper.Map<ProblemCommentViewModel[]>(comments)) : BadRequest();
    }

    [HttpGet("/SolveComments")]
    public async Task<ActionResult<SolveCommentViewModel[]>> GetSolveComments([FromQuery] int solveId)
    {
        var comments = await _commentService.GetSolveComments(solveId);

        return comments != null ? Ok(_mapper.Map<SolveCommentViewModel[]>(comments)) : BadRequest();
    }

    [HttpPost("/LessonComments")]
    [Authorize(Roles = "User,Administrator")]
    public async Task<ActionResult<int?>> AddLessonComment([FromForm] LessonCommentCreateViewModel comment)
    {
        string? userId = User.FindFirstValue("Id");

        if (userId == null)
        {
            return Unauthorized();
        }

        LessonCommentCreateModel model = _mapper.Map<LessonCommentCreateModel>(comment);
        model.AuthorId = Guid.Parse(userId!);

        int? result = await _commentService.AddLessonComment(model);

        return result != null ? Ok(result) : BadRequest();
    }

    [HttpPost("/ProblemComments")]
    [Authorize(Roles = "User,Administrator")]
    public async Task<ActionResult<int?>> AddProblemComment([FromForm] ProblemCommentCreateViewModel comment)
    {
        string? userId = User.FindFirstValue("Id");

        if (userId == null)
        {
            return Unauthorized();
        }

        ProblemCommentCreateModel model = _mapper.Map<ProblemCommentCreateModel>(comment);
        model.AuthorId = Guid.Parse(userId!);

        int? result = await _commentService.AddProblemComment(model);

        return result != null ? Ok(result) : BadRequest();
    }

    [HttpPost("/SolveComments")]
    [Authorize(Roles = "User,Administrator")]
    public async Task<ActionResult<int?>> AddSolveComment([FromForm] SolveCommentCreateViewModel comment)
    {
        string? userId = User.FindFirstValue("Id");

        if (userId == null)
        {
            return Unauthorized();
        }

        SolveCommentCreateModel model = _mapper.Map<SolveCommentCreateModel>(comment);
        model.AuthorId = Guid.Parse(userId!);

        int? result = await _commentService.AddSolveComment(model);

        return result != null ? Ok(result) : BadRequest();
    }
}
