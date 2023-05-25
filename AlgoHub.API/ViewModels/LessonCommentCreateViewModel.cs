namespace AlgoHub.API.Models;

public class LessonCommentCreateViewModel
{
    public int LessonId { get; set; }
    public int? ParentCommentId { get; set; }
    public string Content { get; set; }
}
