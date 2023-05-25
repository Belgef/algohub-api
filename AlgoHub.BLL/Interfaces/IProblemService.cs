using AlgoHub.API.Models;
using AlgoHub.DAL.Entities;

namespace AlgoHub.BLL.Interfaces;

public interface IProblemService
{
    Task<int?> AddProblem(ProblemCreateModel problem);
    Task<ProblemModel?> GetProblemById(int problemid);
    Task<ProblemModel[]> GetProblems();
    Task<int?> AddProblemVote(int problemId, Guid authorId, bool isUpvote);
    Task<bool?> GetProblemVote(int problemId, Guid authorId);
}
