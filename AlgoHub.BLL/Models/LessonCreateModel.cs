using Microsoft.AspNetCore.Http;

namespace AlgoHub.API.Models;

public class LessonCreateModel
{
    public string Title { get; set; } = null!;
    public string LessonContent { get; set; } = null!;
    public Guid AuthorId { get; set; }
    public IFormFile? Image { get; set; }
    public string[]? Tags { get; set; }
}
