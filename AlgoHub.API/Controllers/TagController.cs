using AlgoHub.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AlgoHub.API.Controllers;

[ApiController]
[Route("[controller]/")]
public class TagController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet]
    public async Task<ActionResult<string[]?>> GetAllTags()
    {
        string[]? result = await _tagService.GetAllTags();

        return result != null ? Ok(result) : BadRequest();
    }

    [HttpGet("Lesson/{lessonId}")]
    public async Task<ActionResult<string[]?>> GetLessonTags(int lessonId)
    {
        string[]? result = await _tagService.GetLessonTags(lessonId);

        return result != null ? Ok(result) : BadRequest();
    }

    [HttpGet("Problem/{problemId}")]
    public async Task<ActionResult<string[]?>> GetProblemTags(int problemId)
    {
        string[]? result = await _tagService.GetProblemTags(problemId);

        return result != null ? Ok(result) : BadRequest();
    }
}
