using AlgoHub.DAL.Entities;

namespace AlgoHub.DAL.Interfaces;

public interface ILessonRepository
{
    Task<int?> AddLesson(Lesson problem);
    Task<Lesson?> GetLessonById(int lessonId);
    Task<Lesson[]> GetLessons();
    Task<int?> AddLessonVote(int lessonId, Guid authorId, bool isUpvote);
    Task<bool?> GetLessonVote(int lessonId, Guid authorId);
}