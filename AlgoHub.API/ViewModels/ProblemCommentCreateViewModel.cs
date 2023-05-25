namespace AlgoHub.API.Models;

public class ProblemCommentCreateViewModel
{
    public int ProblemId { get; set; }
    public int? ParentCommentId { get; set; }
    public string Content { get; set; }
}
