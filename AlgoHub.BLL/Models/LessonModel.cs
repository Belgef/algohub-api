namespace AlgoHub.API.Models;

public class LessonModel
{
    public int? LessonId { get; set; }
    public string? Title { get; set; }
    public ContentElement[]? LessonContent { get; set; }
    public UserModel? Author { get; set; }
    public string? ImageName { get; set; }
    public int? Views { get; set; }
    public int? Upvotes { get; set; }
    public int? Downvotes { get; set; }
    public DateTime? CreateDate { get; set; }
}