using AlgoHub.DAL.Entities;

namespace AlgoHub.DAL.Interfaces;

public interface IProblemRepository
{
    Task<int?> AddProblem(Problem problem);
    Task<Problem?> GetProblemById(int problemId);
    Task<Problem[]> GetProblems();
}
