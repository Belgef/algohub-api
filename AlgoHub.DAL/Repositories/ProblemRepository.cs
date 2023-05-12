using AlgoHub.DAL.Context;
using AlgoHub.DAL.Entities;
using AlgoHub.DAL.Interfaces;
using Dapper;
using System.Data;

namespace AlgoHub.DAL.Repositories;

public class ProblemRepository : IProblemRepository
{
    private readonly AlgoHubDbContext _context;

    public ProblemRepository(AlgoHubDbContext context)
    {
        _context = context;
    }

    public async Task<Problem?> GetProblemById(int problemId)
    {
        var parameters = new DynamicParameters(new { ProblemId = problemId });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<Problem?, User?, Problem>(
            "spGetProblemById",
            (p, u) =>
            {
                if (p != null)
                {
                    p.Author ??= u;
                }

                return p;
            },
            parameters,
            splitOn: "UserName",
            commandType: CommandType.StoredProcedure);

        return result.FirstOrDefault();
    }

    public async Task<Problem[]> GetProblems()
    {
        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<Problem, User?, Problem>(
            "spGetProblems",
            (p, u) =>
            {
                if (p != null)
                {
                    p.Author ??= u;
                }

                return p;
            },
            splitOn: "UserName",
            commandType: CommandType.StoredProcedure);

        return result.ToArray();
    }

    public async Task<int?> AddProblem(Problem problem)
    {
        var parameters = new DynamicParameters(new { 
            problem.ProblemName,
            problem.ProblemContent,
            AuthorId = problem.Author?.UserId,
            problem.ImageName,
            problem.TimeLimitMs,
            problem.MemoryLimitBytes
        });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<int>("spAddProblem", parameters, commandType: CommandType.StoredProcedure);

        return result.FirstOrDefault();
    }
}