using Microsoft.AspNetCore.Http;

namespace AlgoHub.API.Models;

public class ProblemCreateModel
{
    public string ProblemName { get; set; } = null!;
    public string ProblemContent { get; set; } = null!;
    public Guid AuthorId { get; set; }
    public IFormFile? Image { get; set; }
    public int TimeLimitMs { get; set; }
    public int MemoryLimitBytes { get; set; }
    public TestModel[] Tests { get; set; } = null!;
}
