using AlgoHub.DAL.Context;
using AlgoHub.DAL.Entities;
using AlgoHub.DAL.Interfaces;
using Dapper;
using System.Data;

namespace AlgoHub.DAL.Repositories;

public class TestRepository : ITestRepository
{
    private readonly AlgoHubDbContext _context;

    public TestRepository(AlgoHubDbContext context)
    {
        _context = context;
    }

    public async Task<Test[]> GetProblemTests(int problemId)
    {
        var parameters = new DynamicParameters(new
        {
            ProblemId = problemId,
        });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<Test>("spGetProblemTests", parameters, commandType: CommandType.StoredProcedure);

        return result.ToArray();
    }

    public async Task<int?> AddTest(Test test, int problemId)
    {
        var parameters = new DynamicParameters(new
        {
            ProblemId = problemId,
            test.Input,
            test.Output,
        });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<int?>("spAddTest", parameters, commandType: CommandType.StoredProcedure);

        return result.FirstOrDefault();
    }
}