using System.ComponentModel.DataAnnotations;

namespace AlgoHub.API.ViewModels;

public class SolveCreateViewModel
{
    public int ProblemId { get; set; }
    public string LanguageName { get; set; }
    public string Code { get; set; } = null!;
}
