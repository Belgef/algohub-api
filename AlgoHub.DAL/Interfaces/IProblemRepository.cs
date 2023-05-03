using AlgoHub.DAL.Entities;

namespace AlgoHub.DAL.Interfaces;

public interface IProblemRepository
{
    Task<ProblemDetailed?> GetProblemById(int problemId);
}