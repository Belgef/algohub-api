using AlgoHub.API.Models;

namespace AlgoHub.BLL.Interfaces;

public interface ILessonService
{
    Task<int?> AddLesson(LessonCreateModel lesson);
    Task<bool> DeleteLesson(int lessonId);
    Task<LessonModel?> GetLessonById(int lessonid);
    Task<LessonModel[]> GetLessons(bool deleted = false);
    Task<bool> RetrieveLesson(int lessonId);
}
