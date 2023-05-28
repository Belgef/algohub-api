using AlgoHub.DAL.Context;
using AlgoHub.DAL.Interfaces;
using Dapper;
using System.Data;

namespace AlgoHub.DAL.Repositories;

public class TagRepository : ITagRepository
{
    private readonly AlgoHubDbContext _context;

    public TagRepository(AlgoHubDbContext context)
    {
        _context = context;
    }

    public async Task<int?> AddLessonTag(string tag, int lessonId)
    {
        var parameters = new DynamicParameters(new
        {
            LessonId = lessonId,
            Tag = tag
        });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<int>("spAddLessonTag", parameters, commandType: CommandType.StoredProcedure);

        return result.FirstOrDefault();
    }

    public async Task<int?> AddProblemTag(string tag, int problemId)
    {
        var parameters = new DynamicParameters(new
        {
            ProblemId = problemId,
            Tag = tag
        });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<int>("spAddProblemTag", parameters, commandType: CommandType.StoredProcedure);

        return result.FirstOrDefault();
    }

    public async Task<string[]?> GetAllTags()
    {
        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<string>(
            "spGetTags",
            commandType: CommandType.StoredProcedure);

        return result.ToArray();
    }

    public async Task<string[]?> GetLessonTags(int lessonId)
    {
        var parameters = new DynamicParameters(new { LessonId = lessonId });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<string>(
            "spGetLessonTags",
            parameters,
            commandType: CommandType.StoredProcedure);

        return result.ToArray();
    }

    public async Task<string[]?> GetProblemTags(int problemId)
    {
        var parameters = new DynamicParameters(new { ProblemId = problemId });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<string>(
            "spGetProblemTags",
            parameters,
            commandType: CommandType.StoredProcedure);

        return result.ToArray();
    }
}
