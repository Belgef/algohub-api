using AlgoHub.API.Models;

namespace AlgoHub.BLL.Interfaces;

public interface ICommentService
{
    Task<int?> AddLessonComment(LessonCommentCreateModel comment);
    Task<int?> AddProblemComment(ProblemCommentCreateModel comment);
    Task<int?> AddSolveComment(SolveCommentCreateModel comment);
    Task<LessonCommentModel[]?> GetLessonComments(int lessonId);
    Task<ProblemCommentModel[]?> GetProblemComments(int problemId);
    Task<SolveCommentModel[]?> GetSolveComments(int solveId);
}