namespace AlgoHub.API.ViewModels;

public class SolveCommentViewModel
{
    public int? SolveCommentId { get; set; }
    public int? SolveId { get; set; }
    public UserViewModel? Author { get; set; }
    public string? Content { get; set; }
    public DateTime? CreateDate { get; set; }
    public SolveCommentViewModel[]? Replies { get; set; }
}