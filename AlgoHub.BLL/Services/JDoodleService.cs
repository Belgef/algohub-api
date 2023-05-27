using AlgoHub.API.Models;
using AlgoHub.BLL.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace AlgoHub.BLL.Services;

public class JDoodleService : ICompilerService
{
    private const string Path = "https://api.jdoodle.com/v1/execute";
    private readonly Dictionary<string, (string language, int versionIndex)> _langMap = new()
    {
        { "cpp", ("cpp", 5) },
        { "csharp", ("csharp", 4) },
        { "java", ("java", 4) },
        { "javascript", ("nodejs", 4) },
        { "php", ("php", 4) },
        { "python", ("python3", 4) },
    };

    private readonly string? _clientId;
    private readonly string? _clientSecret;
    private readonly HttpClient _httpClient;

    public JDoodleService(IConfiguration config, IHttpClientFactory clientFactory)
    {
        _clientId = config["JDoodle:ClientId"];
        _clientSecret = config["JDoodle:Secret"];
        _httpClient = clientFactory.CreateClient();
    }

    public async Task<CompilationResult?> Compile(string code, string language, string input)
    {
        var response = await _httpClient.PostAsJsonAsync(Path, new
        {
            clientId = _clientId,
            clientSecret = _clientSecret,
            script = code,
            stdin = input,
            _langMap[language].language,
            _langMap[language].versionIndex
        });

        var result = await response.Content.ReadFromJsonAsync<CompilationResultRaw>(new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        return result == null ? null : new CompilationResult()
        {
            Output = result.Output,
            Memory = result.Memory.ParseIntOrNull(),
            CpuTime = (int?)result.CpuTime.ParseDoubleOrNull() * 1000,
            Error = result.Error,
            StatusCode = result.StatusCode
        };
    }
}