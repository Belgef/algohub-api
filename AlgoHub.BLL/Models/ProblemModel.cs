using AlgoHub.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace AlgoHub.API.Models;

public class ProblemModel
{
    public int? ProblemId { get; set; }
    public string? ProblemName { get; set; }
    public string? ProblemContent { get; set; }
    public UserModel? Author { get; set; }
    public string? ImageName { get; set; }
    public int? Views { get; set; }
    public int? Solves { get; set; }
    public int? Upvotes { get; set; }
    public int? Downvotes { get; set; }
    public int? TimeLimitMs { get; set; }
    public int? MemoryLimitBytes { get; set; }
    public DateTime? CreateDate { get; set; }
}
