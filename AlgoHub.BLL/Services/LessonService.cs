using AlgoHub.API.Models;
using AlgoHub.BLL.Interfaces;
using AlgoHub.DAL;
using AlgoHub.DAL.Entities;
using AutoMapper;
using System.Text.Json;

namespace AlgoHub.BLL.Services;

public class LessonService : ILessonService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStorageService _storageService;
    private readonly IMapper _mapper;

    public LessonService(IUnitOfWork unitOfWork, IStorageService storageService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _storageService = storageService;
    }

    public async Task<LessonModel?> GetLessonById(int lessonId)
    {
        var result = await _unitOfWork.LessonRepository.GetLessonById(lessonId);

        if(result == null)
        {
            return null;
        }

        var model = _mapper.Map<LessonModel>(result);

        model.LessonContent = JsonSerializer.Deserialize<ContentElement[]>(result.LessonContent ?? "[]", new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        return model;
    }

    public async Task<LessonModel[]> GetLessons()
    {
        var result = await _unitOfWork.LessonRepository.GetLessons();

        return _mapper.Map<LessonModel[]>(result);
    }

    public async Task<int?> AddLesson(LessonCreateModel lesson)
    {
        var newLesson = _mapper.Map<Lesson>(lesson);
        newLesson.Author = new User() { UserId = lesson.AuthorId };
        newLesson.ImageName = await _storageService.SaveFile(lesson.Image);

        return await _unitOfWork.LessonRepository.AddLesson(newLesson);
    }

    public Task<int?> AddLessonVote(int lessonId, Guid userId, bool isUpvote)
        => _unitOfWork.LessonRepository.AddLessonVote(lessonId, userId, isUpvote);

    public Task<bool?> GetLessonVote(int lessonId, Guid userId)
        => _unitOfWork.LessonRepository.GetLessonVote(lessonId, userId);
}