using AlgoHub.BLL.Interfaces;
using AlgoHub.DAL;

namespace AlgoHub.BLL.Services;

public class VoteService : IVoteService
{
    private readonly IUnitOfWork _unitOfWork;

    public VoteService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<int?> AddLessonVote(int lessonId, Guid userId, bool isUpvote)
        => _unitOfWork.VoteRepository.AddLessonVote(lessonId, userId, isUpvote);

    public Task<bool?> GetLessonVote(int lessonId, Guid userId)
        => _unitOfWork.VoteRepository.GetLessonVote(lessonId, userId);

    public Task<int?> AddProblemVote(int problemId, Guid authorId, bool isUpvote)
        => _unitOfWork.VoteRepository.AddProblemVote(problemId, authorId, isUpvote);

    public Task<bool?> GetProblemVote(int problemId, Guid authorId)
        => _unitOfWork.VoteRepository.GetProblemVote(problemId, authorId);

    public Task<int?> AddSolveVote(int solveId, Guid authorId, bool isUpvote)
        => _unitOfWork.VoteRepository.AddSolveVote(solveId, authorId, isUpvote);

    public Task<bool?> GetSolveVote(int solveId, Guid authorId)
        => _unitOfWork.VoteRepository.GetSolveVote(solveId, authorId);
}