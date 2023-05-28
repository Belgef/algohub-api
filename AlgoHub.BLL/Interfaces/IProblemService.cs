using AlgoHub.API.Models;
using AlgoHub.DAL.Entities;

namespace AlgoHub.BLL.Interfaces;

public interface IProblemService
{
    Task<int?> AddProblem(ProblemCreateModel problem);
    Task<bool> DeleteProblem(int problemId);
    Task<ProblemModel?> GetProblemById(int problemid);
    Task<ProblemModel[]> GetProblems(bool deleted = false);
    Task<bool> RetrieveProblem(int problemId);
}
