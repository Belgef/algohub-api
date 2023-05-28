using AlgoHub.DAL.Entities;

namespace AlgoHub.DAL.Interfaces;

public interface ILessonRepository
{
    Task<int?> AddLesson(Lesson problem);
    Task<bool> DeleteLesson(int lessonId);
    Task<Lesson?> GetLessonById(int lessonId);
    Task<Lesson[]> GetLessons(bool deleted = false);
    Task<bool> RetrieveLesson(int lessonId);
}