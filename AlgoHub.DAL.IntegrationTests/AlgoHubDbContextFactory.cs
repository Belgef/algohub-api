using AlgoHub.DAL.Context;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AlgoHub.DAL.IntegrationTests;

internal static class AlgoHubDbContextFactory
{
    private static readonly string _connectionString = "Data Source=LENOVOIDEAPAD;Database=AlgoHubTest;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
    private static readonly string _masterConnectionString = "Data Source=LENOVOIDEAPAD;Database=AlgoHubTest;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    private static AlgoHubDbContext? _instance;

    public static AlgoHubDbContext Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }

            RunSetupScript();

            RunScriptsInDirectory("C:\\Users\\girny\\OneDrive\\Documents\\algohub-db\\schema", _connectionString);
            RunScriptsInDirectory("C:\\Users\\girny\\OneDrive\\Documents\\algohub-db\\procedures", _connectionString);


            _instance = new AlgoHubDbContext(_connectionString);

            return _instance;
        }
    }

    public static void RunStartScript()
    {
        using var connection = Instance.CreateConnection();

        connection.Open();

        var scriptText = File.ReadAllText($"../../../Scripts/StartScript.sql");

        var batches = scriptText.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var batch in batches)
        {
            connection.Execute(batch);
        }
    }

    private static void RunSetupScript()
    {
        using var connection = new SqlConnection(_masterConnectionString);

        connection.Open();

        var scriptText = File.ReadAllText($"../../../Scripts/SetupScript.sql");

        var batches = scriptText.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var batch in batches)
        {
            connection.Execute(batch);
        }
    }

    public static void RunScriptsInDirectory(string directoryPath, string connectionString)
    {
        var scripts = Directory.GetFiles(directoryPath, "*.sql");

        using var connection = new SqlConnection(connectionString);

        connection.Open();

        foreach (var script in scripts)
        {
            var scriptText = File.ReadAllText(script);

            var batches = scriptText.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var batch in batches)
            {
                connection.Execute(batch);
            }
        }
    }
}