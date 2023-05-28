using AlgoHub.DAL.Entities;

namespace AlgoHub.DAL.Interfaces;

public interface ISolveRepository
{
    Task<int?> AddSolve(Solve solve);
    Task<Solve[]> GetSolves(int problemId);
}