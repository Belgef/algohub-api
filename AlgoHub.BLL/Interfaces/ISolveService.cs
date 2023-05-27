using AlgoHub.API.Models;

namespace AlgoHub.BLL.Interfaces
{
    public interface ISolveService
    {
        Task<int?> AddSolveVote(int solveId, Guid authorId, bool isUpvote);
        Task<bool?> GetSolveVote(int solveId, Guid authorId);
        Task<SolveModel[]> GetSolves(int problemId);
        Task<string[]?> VerifyAndAddSolve(SolveCreateModel solve);
    }
}