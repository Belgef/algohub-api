namespace AlgoHub.API.ViewModels;

public class ProblemCommentViewModel
{
    public int? ProblemCommentId { get; set; }
    public int? ProblemId { get; set; }
    public UserViewModel? Author { get; set; }
    public string? Content { get; set; }
    public DateTime? CreateDate { get; set; }
    public ProblemCommentViewModel[]? Replies { get; set; }
}
