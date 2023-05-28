using AlgoHub.DAL.Context;
using AlgoHub.DAL.Interfaces;
using Dapper;
using System.Data;

namespace AlgoHub.DAL.Repositories;

public class VoteRepository : IVoteRepository
{
    private readonly AlgoHubDbContext _context;

    public VoteRepository(AlgoHubDbContext context)
    {
        _context = context;
    }

    public async Task<int?> AddLessonVote(int lessonId, Guid authorId, bool isUpvote)
    {
        var parameters = new DynamicParameters(new
        {
            LessonId = lessonId,
            AuthorId = authorId,
            IsUpvote = isUpvote
        });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<int?>("spAddLessonVote", parameters, commandType: CommandType.StoredProcedure);

        return result.FirstOrDefault();
    }

    public async Task<bool?> GetLessonVote(int lessonId, Guid authorId)
    {
        var parameters = new DynamicParameters(new
        {
            LessonId = lessonId,
            AuthorId = authorId
        });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<bool?>("spGetLessonVote", parameters, commandType: CommandType.StoredProcedure);

        return result.FirstOrDefault();
    }

    public async Task<int?> AddProblemVote(int problemId, Guid authorId, bool isUpvote)
    {
        var parameters = new DynamicParameters(new
        {
            ProblemId = problemId,
            AuthorId = authorId,
            IsUpvote = isUpvote
        });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<int?>("spAddProblemVote", parameters, commandType: CommandType.StoredProcedure);

        return result.FirstOrDefault();
    }

    public async Task<bool?> GetProblemVote(int problemId, Guid authorId)
    {
        var parameters = new DynamicParameters(new
        {
            ProblemId = problemId,
            AuthorId = authorId
        });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<bool?>("spGetProblemVote", parameters, commandType: CommandType.StoredProcedure);

        return result.FirstOrDefault();
    }

    public async Task<int?> AddSolveVote(int solveId, Guid authorId, bool isUpvote)
    {
        var parameters = new DynamicParameters(new
        {
            SolveId = solveId,
            AuthorId = authorId,
            IsUpvote = isUpvote
        });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<int?>("spAddSolveVote", parameters, commandType: CommandType.StoredProcedure);

        return result.FirstOrDefault();
    }

    public async Task<bool?> GetSolveVote(int solveId, Guid authorId)
    {
        var parameters = new DynamicParameters(new
        {
            SolveId = solveId,
            AuthorId = authorId
        });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<bool?>("spGetSolveVote", parameters, commandType: CommandType.StoredProcedure);

        return result.FirstOrDefault();
    }
}