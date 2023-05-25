using AlgoHub.API.Models;

namespace AlgoHub.BLL.Interfaces;

public interface ILessonService
{
    Task<int?> AddLesson(LessonCreateModel lesson);
    Task<LessonModel?> GetLessonById(int lessonid);
    Task<LessonModel[]> GetLessons();
    Task<int?> AddLessonVote(int lessonId, Guid userId, bool isUpvote);
    Task<bool?> GetLessonVote(int lessonId, Guid userId);
}
