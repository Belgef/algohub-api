namespace AlgoHub.BLL.Interfaces
{
    public interface ITagService
    {
        Task<string[]?> GetAllTags();
        Task<string[]?> GetLessonTags(int lessonId);
        Task<string[]?> GetProblemTags(int problemId);
    }
}