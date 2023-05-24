using AlgoHub.DAL.Entities;

namespace AlgoHub.DAL.Interfaces;

public interface ILessonRepository
{
    Task<int?> AddLesson(Lesson problem);
    Task<Lesson?> GetLessonById(int lessonId);
    Task<Lesson[]> GetLessons();
}