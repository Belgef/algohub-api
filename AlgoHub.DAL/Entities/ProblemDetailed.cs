namespace AlgoHub.DAL.Entities;

public class ProblemDetailed
{
    public int? ProblemId { get; set; }
    public string? ProblemName { get; set; }
    public string? ProblemContentFileName { get; set; }
    public string? AuthorName { get; set; }
    public string? AuthorUserName { get; set; }
    public string? AuthorIconName { get; set; }
    public string? ImageName { get; set; }
    public int? Views { get; set; }
    public int? Solves { get; set; }
    public int? Upvotes { get; set; }
    public int? Downvotes { get; set; }
    public int? TimeLimitMs { get; set; }
    public int? MemoryLimitBytes { get; set; }
    public DateTime? CreateDate { get; set; }
}