using AlgoHub.API.Models;
using AlgoHub.API.ViewModels;
using AlgoHub.BLL.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AlgoHub.API.Controllers;

[ApiController]
[Route("[controller]/")]
public class LessonController : ControllerBase
{
    private readonly ILessonService _lessonService;
    private readonly IMapper _mapper;

    public LessonController(ILessonService lessonService, IMapper mapper)
    {
        _lessonService = lessonService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<LessonViewModel[]>> GetAll()
    {
        var lessons = await _lessonService.GetLessons();

        return lessons != null ? Ok(_mapper.Map<LessonViewModel[]>(lessons)) : BadRequest();
    }

    [HttpGet("{problemId}")]
    public async Task<ActionResult<LessonViewModel>> Get(int problemId)
    {
        var lesson = await _lessonService.GetLessonById(problemId);

        return lesson != null ? Ok(lesson) : NotFound();
    }

    [HttpPost]
    [Authorize(Roles = "User")]
    public async Task<ActionResult<int?>> AddLesson([FromForm] LessonCreateViewModel lesson)
    {
        string? userId = User.FindFirstValue("Id");

        if (userId == null)
        {
            return Unauthorized();
        }

        LessonCreateModel model = _mapper.Map<LessonCreateModel>(lesson);
        model.AuthorId = Guid.Parse(userId!);

        int? result = await _lessonService.AddLesson(model);

        return result != null ? Ok(result) : BadRequest();
    }
}