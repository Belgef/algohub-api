using AlgoHub.API.Models;
using AlgoHub.BLL.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System.Buffers.Text;
using System.Collections.Generic;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;

namespace AlgoHub.BLL.Services;

public class RextesterCompilationResult
{
    public string? Result { get; set; }
    public string? Stats { get; set; }
    public string? Warnings { get; set; }
    public string? Errors { get; set; }
}

public class RextesterService : ICompilerService
{
    private const string Path = "https://rextester.com/rundotnet/api";
    private readonly Dictionary<string, int> _langMap = new()
    {
        { "cpp", 27 },
        { "csharp", 1 },
        { "java", 4 },
        { "javascript", 17 },
        { "php", 8 },
        { "python", 24 },
    };

    private readonly string? _apiKey;
    private readonly HttpClient _httpClient;

    public RextesterService(IConfiguration config, IHttpClientFactory clientFactory)
    {
        _apiKey = config["Rextester:Secret"];
        _httpClient = clientFactory.CreateClient();
    }

    public async Task<CompilationResult?> Compile(string code, string language, string input)
    {
        var response = await _httpClient.PostAsJsonAsync(Path, new
        {
            LanguageChoice = _langMap[language],
            Program = code,
            Input = input,
            CompilerArgs = "",
            ApiKey = _apiKey
        });

        var result = await response.Content.ReadFromJsonAsync<RextesterCompilationResult>(new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        return result == null ? null : new CompilationResult()
        {
            Output = result.Result,
            Memory = (int?)(Regex.Match(result.Stats,@"memory peak: (\d+([,.]\d+)?) Mb").Groups[1].Value.ParseDoubleOrNull() * 1024 * 1024),
            CpuTime = (int?)(Regex.Match(result.Stats, @"bsolute running time: (\d+([,.]\d+)?) sec").Groups[1].Value.Replace('.',',').ParseDoubleOrNull() * 1000),
            Error = result.Errors,
            StatusCode = (int)response.StatusCode
        };
    }
}