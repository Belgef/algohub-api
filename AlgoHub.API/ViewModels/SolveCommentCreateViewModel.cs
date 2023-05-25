namespace AlgoHub.API.Models;

public class SolveCommentCreateViewModel
{
    public int SolveId { get; set; }
    public int? ParentCommentId { get; set; }
    public string Content { get; set; }
}