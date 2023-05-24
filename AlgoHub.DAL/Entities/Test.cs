namespace AlgoHub.DAL.Entities;

public class Test
{
    public int? TestId { get; set; }
    public int ProblemId { get; set; }
    public string? Input { get;  set; }
    public string? Output { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}