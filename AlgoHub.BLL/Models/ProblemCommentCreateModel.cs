namespace AlgoHub.API.Models;

public class ProblemCommentCreateModel
{
    public int ProblemId { get; set; }
    public int? ParentCommentId { get; set; }
    public Guid AuthorId { get; set; }
    public string Content { get; set; }
}
