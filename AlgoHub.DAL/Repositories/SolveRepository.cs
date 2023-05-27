using AlgoHub.DAL.Context;
using AlgoHub.DAL.Entities;
using AlgoHub.DAL.Interfaces;
using Dapper;
using System.Data;

namespace AlgoHub.DAL.Repositories;

public class SolveRepository : ISolveRepository
{
    private readonly AlgoHubDbContext _context;

    public SolveRepository(AlgoHubDbContext context)
    {
        _context = context;
    }

    public async Task<int?> AddSolve(Solve solve)
    {
        var parameters = new DynamicParameters(new
        {
            solve.ProblemId,
            AuthorId = solve.Author?.UserId,
            solve.Language?.LanguageName,
            solve.Code,
            solve.TimeMs,
            solve.MemoryBytes
        });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<int?>("spAddSolve", parameters, commandType: CommandType.StoredProcedure);

        return result.FirstOrDefault();
    }

    public async Task<Solve[]> GetSolves(int problemId)
    {
        var parameters = new DynamicParameters(new
        {
            ProblemId = problemId
        });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<Solve?, User?, Language?, Solve>(
            "spGetSolves",
            (s, u, l) =>
            {
                if (s != null)
                {
                    s.Author = u;
                    s.Language = l;
                }

                return s;
            },
            parameters,
            splitOn: "UserName, LanguageName",
            commandType: CommandType.StoredProcedure);

        return result.ToArray();
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

    public async Task<Solve[]> GetProblemTests(int problemId)
    {
        var parameters = new DynamicParameters(new
        {
            ProblemId = problemId
        });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<Solve?, User?, Language?, Solve>(
            "spGetSolves",
            (s, u, l) =>
            {
                if (s != null)
                {
                    s.Author = u;
                    s.Language = l;
                }

                return s;
            },
            parameters,
            splitOn: "UserName, LanguageName",
            commandType: CommandType.StoredProcedure);

        return result.ToArray();
    }
}
