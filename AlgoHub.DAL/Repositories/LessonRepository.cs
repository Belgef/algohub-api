using AlgoHub.DAL.Context;
using AlgoHub.DAL.Entities;
using AlgoHub.DAL.Interfaces;
using Dapper;
using System.Data;

namespace AlgoHub.DAL.Repositories;

public class LessonRepository : ILessonRepository
{
    private readonly AlgoHubDbContext _context;

    public LessonRepository(AlgoHubDbContext context)
    {
        _context = context;
    }

    public async Task<Lesson?> GetLessonById(int lessonId)
    {
        var parameters = new DynamicParameters(new { LessonId = lessonId });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<Lesson?, User?, Lesson>(
            "spGetLessonById",
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

    public async Task<Lesson[]> GetLessons()
    {
        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<Lesson?, User?, Lesson>(
            "spGetLessons",
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

    public async Task<int?> AddLesson(Lesson lesson)
    {
        var parameters = new DynamicParameters(new
        {
            lesson.Title,
            lesson.LessonContent,
            AuthorId = lesson.Author?.UserId,
            lesson.ImageName
        });

        using var connection = _context.CreateConnection();

        var result = await connection.QueryAsync<int>("spAddLesson", parameters, commandType: CommandType.StoredProcedure);

        return result.FirstOrDefault();
    }
}