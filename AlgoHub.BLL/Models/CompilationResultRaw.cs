namespace AlgoHub.API.Models;

public class CompilationResultRaw
{
    public string? Output { get; set; }
    public string? CpuTime { get; set; }
    public string? Memory { get; set; }
    public string? Error { get; set; }
    public int StatusCode { get; set; }
}
