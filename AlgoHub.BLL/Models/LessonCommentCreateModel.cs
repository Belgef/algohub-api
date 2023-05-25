namespace AlgoHub.API.Models;

public class LessonCommentCreateModel
{
    public int LessonId { get; set; }
    public int? ParentCommentId { get; set; }
    public Guid AuthorId { get; set; }
    public string Content { get; set; }
}
