using AlgoHub.DAL.Interfaces;

namespace AlgoHub.DAL;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IProblemRepository ProblemRepository { get; }
    ILessonRepository LessonRepository { get; }
    ICommentRepository CommentRepository { get; }
}
