namespace AlgoHub.DAL.Interfaces;

public interface ITagRepository
{
    Task<int?> AddLessonTag(string tag, int lessonId);
    Task<int?> AddProblemTag(string tag, int problemId);
    Task<string[]?> GetAllTags();
    Task<string[]?> GetLessonTags(int lessonId);
    Task<string[]?> GetProblemTags(int problemId);
}