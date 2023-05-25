using AlgoHub.DAL.Entities;

namespace AlgoHub.API.Models;

public class SolveCommentModel
{
    public int? SolveCommentId { get; set; }
    public int? SolveId { get; set; }
    public UserModel? Author { get; set; }
    public string? Content { get; set; }
    public DateTime? CreateDate { get; set; }
    public SolveCommentModel[]? Replies { get; set; }
}