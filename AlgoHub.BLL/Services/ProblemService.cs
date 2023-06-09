﻿using AlgoHub.API.Models;
using AlgoHub.API.Services;
using AlgoHub.BLL.Interfaces;
using AlgoHub.DAL;
using AlgoHub.DAL.Entities;
using AutoMapper;
using System.Text.Json;

namespace AlgoHub.BLL.Services;

public class ProblemService : IProblemService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStorageService _storageService;
    private readonly IMapper _mapper;

    public ProblemService(IUnitOfWork unitOfWork, IStorageService storageService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _storageService = storageService;
    }

    public async Task<ProblemModel?> GetProblemById(int problemid)
    {
        var result = await _unitOfWork.ProblemRepository.GetProblemById(problemid);

        if (result == null)
        {
            return null;
        }

        var model = _mapper.Map<ProblemModel>(result);

        model.ProblemContent = JsonSerializer.Deserialize<ContentElement[]>(result.ProblemContent ?? "[]", new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        model.Tags = await _unitOfWork.TagRepository.GetProblemTags(problemid);

        return model;
    }

    public async Task<ProblemModel[]> GetProblems(bool deleted = false)
    {
        var result = await _unitOfWork.ProblemRepository.GetProblems(deleted);

        var problems = _mapper.Map<ProblemModel[]>(result);

        foreach (var problem in problems)
        {
            problem.Tags = await _unitOfWork.TagRepository.GetProblemTags(problem.ProblemId ?? -1);
        }

        return problems;
    }

    public async Task<int?> AddProblem(ProblemCreateModel problem)
    {
        var newProblem = _mapper.Map<Problem>(problem);
        newProblem.Author = new User() { UserId = problem.AuthorId };
        newProblem.ImageName = await _storageService.SaveFile(problem.Image);

        int? problemId = await _unitOfWork.ProblemRepository.AddProblem(newProblem);

        if (problemId == null)
        {
            return null;
        }

        foreach (var test in newProblem.Tests!)
        {
            await _unitOfWork.TestRepository.AddTest(test, problemId ?? -1);
        }

        foreach (var tag in problem.Tags ?? Array.Empty<string>())
        {
            await _unitOfWork.TagRepository.AddProblemTag(tag, problemId ?? -1);
        }

        return problemId;
    }

    public Task<bool> DeleteProblem(int problemId) => _unitOfWork.ProblemRepository.DeleteProblem(problemId);

    public Task<bool> RetrieveProblem(int problemId) => _unitOfWork.ProblemRepository.RetrieveProblem(problemId);
}
