using AlgoHub.DAL.Entities;

namespace AlgoHub.DAL.Interfaces;

public interface ICommentRepository
{
    Task<int?> AddLessonComment(LessonComment comment);
    Task<int?> AddProblemComment(ProblemComment comment);
    Task<int?> AddSolveComment(SolveComment comment);
    Task<LessonComment[]?> GetLessonComments(int lessonId);
    Task<ProblemComment[]?> GetProblemComments(int problemId);
    Task<SolveComment[]?> GetSolveComments(int solveId);
}