using AlgoHub.DAL.Entities;

namespace AlgoHub.BLL.Interfaces;

public interface IProblemService
{
    Task<ProblemDetailed?> GetProblemById(int problemid);
}
