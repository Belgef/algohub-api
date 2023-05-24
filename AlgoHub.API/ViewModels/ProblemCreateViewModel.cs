using AlgoHub.API.Models;
using System.ComponentModel.DataAnnotations;

namespace AlgoHub.API.ViewModels;

public class ProblemCreateViewModel
{
    [RegularExpression(@"[\S ]{5,100}")]
    public string ProblemName { get; set; } = null!;
    public string ProblemContent { get; set; } = null!;
    public IFormFile? Image { get; set; }

    [Range(1, 60000)]
    public int TimeLimitMs { get; set; }

    [Range(1, 10240)]
    public int MemoryLimitBytes { get; set; }
    public string TestsString { get; set; } = null!;
}
