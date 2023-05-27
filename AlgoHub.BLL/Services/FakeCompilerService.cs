using AlgoHub.API.Models;
using AlgoHub.BLL.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace AlgoHub.BLL.Services;

public class FakeCompilerService : ICompilerService
{
    public Task<CompilationResult?> Compile(string code, string language, string input)
    {
        return Task.FromResult(new CompilationResult()
        {
            Output = "",
            Memory = 1,
            CpuTime = 1,
            Error = "",
            StatusCode = -1
        });
    }
}