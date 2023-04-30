using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AlgoHub.DAL.Context;

public class AlgoHubDbContext
{
    private readonly string _connectionString;
    public AlgoHubDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);
}