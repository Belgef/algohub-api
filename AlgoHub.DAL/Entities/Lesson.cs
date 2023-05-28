namespace AlgoHub.DAL.Entities;

public class Lesson
{
    public int? LessonId { get; set; }
    public string? Title { get; set; }
    public string? LessonContent { get; set; }
    public User? Author { get; set; }
    public string? ImageName { get; set; }
    public int? Views { get; set; }
    public int? Upvotes { get; set; }
    public int? Downvotes { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool? Deleted { get; set; }
}