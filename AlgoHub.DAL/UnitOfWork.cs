using AlgoHub.DAL.Context;
using AlgoHub.DAL.Entities;
using AlgoHub.DAL.Interfaces;
using AlgoHub.DAL.Repositories;

namespace AlgoHub.DAL;

public class UnitOfWork : IUnitOfWork
{
    private readonly AlgoHubDbContext _dbContext;

    private IUserRepository? _userRepository;
    private IProblemRepository? _problemRepository;

    public UnitOfWork(AlgoHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IUserRepository UserRepository
        => _userRepository ??= new UserRepository(_dbContext);

    public IProblemRepository ProblemRepository
        => _problemRepository ??= new ProblemRepository(_dbContext);
}
