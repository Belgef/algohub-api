using AlgoHub.API.Models;
using AlgoHub.API.Services;
using AlgoHub.BLL.Interfaces;
using AlgoHub.DAL;
using AlgoHub.DAL.Entities;
using AutoMapper;
using System.Text.Json;

namespace AlgoHub.BLL.Services;

public class SolveService : ISolveService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICompilerService _compilerService;
    private readonly IMapper _mapper;

    public SolveService(IUnitOfWork unitOfWork, ICompilerService compilerService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _compilerService = compilerService;
    }

    public async Task<SolveModel[]> GetSolves(int problemId)
    {
        var result = await _unitOfWork.SolveRepository.GetSolves(problemId);

        return _mapper.Map<SolveModel[]>(result);
    }

    public async Task<string[]?> VerifyAndAddSolve(SolveCreateModel solve)
    {
        var tests = await _unitOfWork.TestRepository.GetProblemTests(solve.ProblemId);
        var problem = await _unitOfWork.ProblemRepository.GetProblemById(solve.ProblemId);

        if (tests == null)
        {
            return null;
        }

        var resultsRaw = await Task.WhenAll(tests.Select(test => _compilerService.Compile(solve.Code, solve.LanguageName, test.Input ?? "")));

        if (resultsRaw == null)
        {
            return null;
        }

        var results = FromResults(resultsRaw, problem?.MemoryLimitBytes ?? -1, problem?.TimeLimitMs ?? -1, tests);

        int? solveId = null;

        if (results.All(r => r.Length == 0) )
        {
            solveId = await AddSolve(
                solve,
                ((int?)resultsRaw.Average(r => r?.CpuTime))
                    ?? (problem?.TimeLimitMs + 1)
                    ?? int.MaxValue,
                ((int?)resultsRaw.Average(r => r?.Memory))
                    ?? (problem?.MemoryLimitBytes + 1)
                    ?? int.MaxValue);
            return solveId == null ? null : results;
        }

        return results;
    }

    private async Task<int?> AddSolve(SolveCreateModel solve, int time, int memory)
    {
        var newSolve = _mapper.Map<Solve>(solve);
        newSolve.Author = new User() { UserId = solve.AuthorId };
        newSolve.Language = new Language() { LanguageName = solve.LanguageName };
        newSolve.TimeMs = time;
        newSolve.MemoryBytes = memory;

        return await _unitOfWork.SolveRepository.AddSolve(newSolve);
    }

    private static string[] FromResults(CompilationResult?[] results, int memoryLimit, int timeLimit, Test[] tests)
        => results.Select((r, i) =>
        r?.StatusCode == -1 ? ""
        : r == null || (r.StatusCode != 200 && r.Error == null) ? "Unknown error occured"
        : r.StatusCode != 200 ? r.Error!
        : r.Memory > memoryLimit ? $"Memory limit reached: {r.Memory} bytes"
        : r.CpuTime > timeLimit ? $"Time limit reached: {r.CpuTime} milliseconds"
        : r.Output?.TrimEnd() != tests[i].Output?.TrimEnd() ? $"Wrong output"
        : ""
        ).ToArray();

    public Task<int?> AddSolveVote(int solveId, Guid authorId, bool isUpvote)
        => _unitOfWork.SolveRepository.AddSolveVote(solveId, authorId, isUpvote);

    public Task<bool?> GetSolveVote(int solveId, Guid authorId)
        => _unitOfWork.SolveRepository.GetSolveVote(solveId, authorId);
}
