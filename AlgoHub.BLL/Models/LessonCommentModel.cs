using AlgoHub.DAL.Entities;

namespace AlgoHub.API.Models;

public class LessonCommentModel
{
    public int? LessonCommentId { get; set; }
    public int? LessonId { get; set; }
    public UserModel? Author { get; set; }
    public string? Content { get; set; }
    public DateTime? CreateDate { get; set; }
    public LessonCommentModel[]? Replies { get; set; }
}
