namespace AlgoHub.API.ViewModels;

public class LessonCommentViewModel
{
    public int? LessonCommentId { get; set; }
    public int? LessonId { get; set; }
    public UserViewModel? Author { get; set; }
    public string? Content { get; set; }
    public DateTime? CreateDate { get; set; }
    public LessonCommentViewModel[]? Replies { get; set; }
}
