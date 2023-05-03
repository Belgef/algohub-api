using AlgoHub.BLL.Interfaces;
using AlgoHub.DAL;
using AlgoHub.DAL.Entities;

namespace AlgoHub.BLL.Services;

public class ProblemService : IProblemService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProblemService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<ProblemDetailed?> GetProblemById(int problemid)
    {
        return _unitOfWork.ProblemRepository.GetProblemById(problemid);
    }
}