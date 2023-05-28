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

    [HttpGet("Deleted")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<LessonViewModel[]>> GetDeleted()
    {
        var lessons = await _lessonService.GetLessons(true);

        return lessons != null ? Ok(_mapper.Map<LessonViewModel[]>(lessons)) : BadRequest();
    }

    [HttpGet("{problemId}")]
    public async Task<ActionResult<LessonViewModel>> Get(int problemId)
    {
        var lesson = await _lessonService.GetLessonById(problemId);

        return lesson != null && (!(lesson.Deleted??false) || User.IsInRole("Administrator")) ? Ok(lesson) : NotFound();
    }

    [HttpPost]
    [Authorize(Roles = "User,Administrator")]
    public async Task<ActionResult<int?>> AddLesson([FromForm] LessonCreateViewModel lesson)
    {
        string? userId = User.FindFirstValue("Id");

        if (userId == null)
        {
            return Unauthorized();
        }

        LessonCreateModel model = _mapper.Map<LessonCreateModel>(lesson);
        model.AuthorId = Guid.Parse(userId!);
        model.Tags = JsonSerializer.Deserialize<string[]>(lesson.TagsString);

        int? result = await _lessonService.AddLesson(model);

        return result != null ? Ok(result) : BadRequest();
    }

    [HttpPost("Delete/{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<bool>> DeleteLesson(int id)
    {
        bool result = await _lessonService.DeleteLesson(id);

        return result ? Ok() : BadRequest();
    }

    [HttpPost("Retrieve/{id}")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<bool>> RetrieveLesson(int id)
    {
        bool result = await _lessonService.RetrieveLesson(id);

        return result ? Ok() : BadRequest();
    }
}