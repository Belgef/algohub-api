using AlgoHub.API.Models;

namespace AlgoHub.BLL.Interfaces;

public interface ILessonService
{
    Task<int?> AddLesson(LessonCreateModel lesson);
    Task<LessonModel?> GetLessonById(int lessonid);
    Task<LessonModel[]> GetLessons();
}
