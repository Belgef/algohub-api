using AlgoHub.API.Models;
using AlgoHub.BLL.Interfaces;
using AlgoHub.DAL;
using AlgoHub.DAL.Entities;
using AutoMapper;

namespace AlgoHub.BLL.Services;

public class TagService : ITagService
{
    private readonly IUnitOfWork _unitOfWork;

    public TagService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<string[]?> GetAllTags()
        => _unitOfWork.TagRepository.GetAllTags();

    public Task<string[]?> GetLessonTags(int lessonId)
        => _unitOfWork.TagRepository.GetLessonTags(lessonId);

    public Task<string[]?> GetProblemTags(int problemId)
        => _unitOfWork.TagRepository.GetProblemTags(problemId);
}
