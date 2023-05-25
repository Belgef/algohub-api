using AlgoHub.DAL.Context;
using AlgoHub.DAL.Entities;
using AlgoHub.DAL.Interfaces;
using Dapper;
using System.Data;

namespace AlgoHub.DAL.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly AlgoHubDbContext _context;

    public CommentRepository(AlgoHubDbContext context)
    {
        _context = context;
    }

    public async Task<int?> AddLessonComment(LessonComment comment)
    {
        var parameters = new DynamicParameters(new
        {
            comment.LessonId,
            comment.ParentCommentId,
            AuthorId = comment.Author?.UserId,
            comment.Content
        });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<int>("spAddLessonComment", parameters, commandType: CommandType.StoredProcedure);

        return result.FirstOrDefault();
    }

    public async Task<int?> AddProblemComment(ProblemComment comment)
    {
        var parameters = new DynamicParameters(new
        {
            comment.ProblemId,
            comment.ParentCommentId,
            AuthorId = comment.Author?.UserId,
            comment.Content
        });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<int>("spAddProblemComment", parameters, commandType: CommandType.StoredProcedure);

        return result.FirstOrDefault();
    }

    public async Task<int?> AddSolveComment(SolveComment comment)
    {
        var parameters = new DynamicParameters(new
        {
            comment.SolveId,
            comment.ParentCommentId,
            AuthorId = comment.Author?.UserId,
            comment.Content
        });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<int>("spAddSolveComment", parameters, commandType: CommandType.StoredProcedure);

        return result.FirstOrDefault();
    }

    public async Task<LessonComment[]?> GetLessonComments(int lessonId)
    {
        var parameters = new DynamicParameters(new { LessonId = lessonId });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<LessonComment?, User?, LessonComment>(
            "spGetLessonCommentsByLessonId",
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

        return result.ToArray();
    }

    public async Task<ProblemComment[]?> GetProblemComments(int problemId)
    {
        var parameters = new DynamicParameters(new { ProblemId = problemId });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<ProblemComment?, User?, ProblemComment>(
            "spGetProblemCommentsByProblemId",
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

        return result.ToArray();
    }

    public async Task<SolveComment[]?> GetSolveComments(int solveId)
    {
        var parameters = new DynamicParameters(new { SolveId = solveId });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<SolveComment?, User?, SolveComment>(
            "spGetSolveCommentsBySolveId",
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

        return result.ToArray();
    }
}
