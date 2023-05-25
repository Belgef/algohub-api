namespace AlgoHub.DAL.Entities;

public class SolveComment
{
    public int? SolveCommentId { get; set; }
    public int? SolveId { get; set; }
    public int? ParentCommentId { get; set; }
    public User? Author { get; set; }
    public string? Content { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}