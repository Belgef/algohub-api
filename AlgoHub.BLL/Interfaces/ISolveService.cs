using AlgoHub.API.Models;

namespace AlgoHub.BLL.Interfaces
{
    public interface ISolveService
    {
        Task<string[]?> VerifyAndAddSolve(SolveCreateModel solve);
        Task<SolveModel[]> GetSolves(int problemId);
    }
}