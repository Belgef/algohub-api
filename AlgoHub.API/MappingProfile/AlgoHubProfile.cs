using AlgoHub.API.Models;
using AlgoHub.API.ViewModels;
using AlgoHub.DAL.Entities;
using AutoMapper;

namespace AlgoHub.API.MappingProfile;

public class AlgoHubProfile : Profile
{
    public AlgoHubProfile()
    {
        CreateMap<UserCreateModel, User>();
        CreateMap<User, UserModel>().ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role == null ? null : src.Role.RoleName));
        CreateMap<ProblemModel, Problem>();
        CreateMap<Problem, ProblemModel>();
        CreateMap<ProblemModel, ProblemViewModel>();
        CreateMap<UserCreateViewModel, UserCreateModel>();
        CreateMap<UserModel, UserViewModel>();
        CreateMap<ProblemCreateModel, Problem>();
        CreateMap<ProblemCreateViewModel, ProblemCreateModel>();
        CreateMap<TestViewModel, TestModel>();
        CreateMap<TestModel, Test>();
        CreateMap<LessonModel, Lesson>();
        CreateMap<Lesson, LessonModel>();
        CreateMap<LessonCreateModel, Lesson>();
        CreateMap<LessonModel, LessonViewModel>();
        CreateMap<LessonCreateModel, Lesson>();
        CreateMap<LessonCreateViewModel, LessonCreateModel>();
    }
}