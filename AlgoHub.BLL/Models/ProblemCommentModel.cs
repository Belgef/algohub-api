using AlgoHub.DAL.Entities;

namespace AlgoHub.API.Models;

public class ProblemCommentModel
{
    public int? ProblemCommentId { get; set; }
    public int? ProblemId { get; set; }
    public UserModel? Author { get; set; }
    public string? Content { get; set; }
    public DateTime? CreateDate { get; set; }
    public ProblemCommentModel[]? Replies { get; set; }
}
