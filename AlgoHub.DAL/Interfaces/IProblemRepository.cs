using AlgoHub.DAL.Entities;

namespace AlgoHub.DAL.Interfaces;

public interface IProblemRepository
{
    Task<int?> AddProblem(Problem problem);
    Task<Problem?> GetProblemById(int problemId);
    Task<Problem[]> GetProblems();
    Task<int?> AddProblemVote(int problemId, Guid authorId, bool isUpvote);
    Task<bool?> GetProblemVote(int problemId, Guid authorId);
}
