using AlgoHub.DAL.Context;
using AlgoHub.DAL.Interfaces;
using AlgoHub.DAL.Repositories;

namespace AlgoHub.DAL;

public class UnitOfWork : IUnitOfWork
{
    private readonly AlgoHubDbContext _dbContext;

    private IUserRepository? _userRepository;
    private IProblemRepository? _problemRepository;
    private ILessonRepository? _lessonRepository;
    private ICommentRepository? _commentRepository;
    private ISolveRepository? _solveRepository;
    private ITestRepository? _testRepository;

    public UnitOfWork(AlgoHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IUserRepository UserRepository
        => _userRepository ??= new UserRepository(_dbContext);

    public IProblemRepository ProblemRepository
        => _problemRepository ??= new ProblemRepository(_dbContext);

    public ILessonRepository LessonRepository
        => _lessonRepository ??= new LessonRepository(_dbContext);

    public ICommentRepository CommentRepository
        => _commentRepository ??= new CommentRepository(_dbContext);

    public ISolveRepository SolveRepository
        => _solveRepository ??= new SolveRepository(_dbContext);

    public ITestRepository TestRepository
        => _testRepository ??= new TestRepository(_dbContext);
}
