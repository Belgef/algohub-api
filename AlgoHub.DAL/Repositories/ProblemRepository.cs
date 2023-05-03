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

    public async Task<ProblemDetailed?> GetProblemById(int problemId)
    {
        var parameters = new DynamicParameters(new { ProblemId = problemId });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<ProblemDetailed?>("spGetProblemById", parameters, commandType: CommandType.StoredProcedure);

        return result.FirstOrDefault();
    }
}