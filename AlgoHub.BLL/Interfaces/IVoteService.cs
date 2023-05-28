namespace AlgoHub.BLL.Interfaces
{
    public interface IVoteService
    {
        Task<int?> AddLessonVote(int lessonId, Guid userId, bool isUpvote);
        Task<int?> AddProblemVote(int problemId, Guid authorId, bool isUpvote);
        Task<int?> AddSolveVote(int solveId, Guid authorId, bool isUpvote);
        Task<bool?> GetLessonVote(int lessonId, Guid userId);
        Task<bool?> GetProblemVote(int problemId, Guid authorId);
        Task<bool?> GetSolveVote(int solveId, Guid authorId);
    }
}