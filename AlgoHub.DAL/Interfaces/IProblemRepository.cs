using AlgoHub.DAL.Entities;

namespace AlgoHub.DAL.Interfaces;

public interface IProblemRepository
{
    Task<int?> AddProblem(Problem problem);
    Task<bool> DeleteProblem(int problemId);
    Task<Problem?> GetProblemById(int problemId);
    Task<Problem[]> GetProblems(bool deleted = false);
    Task<bool> RetrieveProblem(int problemId);
}
