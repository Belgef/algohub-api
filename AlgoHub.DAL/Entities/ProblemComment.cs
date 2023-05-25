namespace AlgoHub.DAL.Entities;

public class ProblemComment
{
    public int? ProblemCommentId { get; set; }
    public int? ProblemId { get; set; }
    public int? ParentCommentId { get; set; }
    public User? Author { get; set; }
    public string? Content { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}
