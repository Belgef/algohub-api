using AlgoHub.DAL.Entities;
using AlgoHub.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace AlgoHub.DAL.Context;

public class AlgoHubDbContext : DbContext
{
    private readonly ILogger<AlgoHubDbContext> _logger;

    public AlgoHubDbContext(DbContextOptions<AlgoHubDbContext> options, ILogger<AlgoHubDbContext> logger) : base(options)
    {
        _logger = logger;
    }

    public Task<T[]> GetSPResultAsync<T>(string name, object parameters)
    {
        string sql = ConstructStoredProcedure(name, parameters);

        _logger.Log(LogLevel.Debug, "Executed sp:", sql);

        return Database.SqlQueryRaw<T>(sql).ToArrayAsync();
    }

    public Task<int> ExecSPAsync(string name, object parameters)
    {
        string sql = ConstructStoredProcedure(name, parameters);

        _logger.Log(LogLevel.Debug, "Executed sp:", sql);

        return Database.ExecuteSqlRawAsync(sql);
    }

    private string ConstructStoredProcedure(string name, object parameters)
    {
        var values = parameters.
            GetType().GetProperties().
            Select(prop => (name: $"@{prop.Name}", value: prop.GetValue(parameters))).
            Where(prop => prop.value != null).Select(prop =>
                $"{prop.name}={(IsNumber(prop.value!) ? prop.value : $"'{prop.value}'")}");

        return $"EXEC {name} {string.Join(", ", values)}";
    }

    private static bool IsNumber(object value)
    {
        return value is sbyte
                || value is byte
                || value is short
                || value is ushort
                || value is int
                || value is uint
                || value is long
                || value is ulong
                || value is float
                || value is double
                || value is decimal;
    }
}