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

        model.Tags = await _unitOfWork.TagRepository.GetLessonTags(lessonId);

        return model;
    }

    public async Task<LessonModel[]> GetLessons(bool deleted = false)
    {
        var result = await _unitOfWork.LessonRepository.GetLessons(deleted);

        var lessons = _mapper.Map<LessonModel[]>(result);

        foreach (var lesson in lessons)
        {
            lesson.Tags = await _unitOfWork.TagRepository.GetLessonTags(lesson?.LessonId ?? -1);
        }

        return lessons;
    }

    public async Task<int?> AddLesson(LessonCreateModel lesson)
    {
        var newLesson = _mapper.Map<Lesson>(lesson);
        newLesson.Author = new User() { UserId = lesson.AuthorId };
        newLesson.ImageName = await _storageService.SaveFile(lesson.Image);

        int? lessonId = await _unitOfWork.LessonRepository.AddLesson(newLesson);

        if (lessonId == null)
        {
            return null;
        }

        foreach (var tag in lesson.Tags ?? Array.Empty<string>())
        {
            await _unitOfWork.TagRepository.AddLessonTag(tag, lessonId ?? -1);
        }

        return lessonId;
    }

    public Task<bool> DeleteLesson(int lessonId) => _unitOfWork.LessonRepository.DeleteLesson(lessonId);

    public Task<bool> RetrieveLesson(int lessonId) => _unitOfWork.LessonRepository.RetrieveLesson(lessonId);
}