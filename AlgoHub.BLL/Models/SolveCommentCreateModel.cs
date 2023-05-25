namespace AlgoHub.API.Models;

public class SolveCommentCreateModel
{
    public int SolveId { get; set; }
    public int? ParentCommentId { get; set; }
    public Guid AuthorId { get; set; }
    public string Content { get; set; }
}