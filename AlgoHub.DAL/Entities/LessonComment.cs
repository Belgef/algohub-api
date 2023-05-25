namespace AlgoHub.DAL.Entities;

public class LessonComment
{
    public int? LessonCommentId { get; set; }
    public int? LessonId { get; set; }
    public int? ParentCommentId { get; set; }
    public User? Author { get; set; }
    public string? Content { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}
