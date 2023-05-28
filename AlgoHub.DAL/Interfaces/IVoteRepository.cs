namespace AlgoHub.DAL.Interfaces
{
    public interface IVoteRepository
    {
        Task<int?> AddLessonVote(int lessonId, Guid authorId, bool isUpvote);
        Task<int?> AddProblemVote(int problemId, Guid authorId, bool isUpvote);
        Task<int?> AddSolveVote(int solveId, Guid authorId, bool isUpvote);
        Task<bool?> GetLessonVote(int lessonId, Guid authorId);
        Task<bool?> GetProblemVote(int problemId, Guid authorId);
        Task<bool?> GetSolveVote(int solveId, Guid authorId);
    }
}