namespace AlgoHub.API.Models;

public class CompilationResult
{
    public string? Output { get; set; }
    public int? CpuTime { get; set; }
    public int? Memory { get; set; }
    public string? Error { get; set; }
    public int StatusCode { get; set; }
}