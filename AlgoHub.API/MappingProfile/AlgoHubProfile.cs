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
        CreateMap<Problem, ProblemModel>().ForMember(dest=>dest.ProblemContent, opt=>opt.Ignore());
        CreateMap<ProblemModel, ProblemViewModel>();
        CreateMap<UserCreateViewModel, UserCreateModel>();
        CreateMap<UserModel, UserViewModel>();
        CreateMap<ProblemCreateModel, Problem>();
        CreateMap<ProblemCreateViewModel, ProblemCreateModel>();
        CreateMap<TestViewModel, TestModel>();
        CreateMap<TestModel, Test>();
        CreateMap<LessonModel, Lesson>();
        CreateMap<Lesson, LessonModel>().ForMember(dest => dest.LessonContent, opt => opt.Ignore());
        CreateMap<LessonCreateModel, Lesson>();
        CreateMap<LessonModel, LessonViewModel>();
        CreateMap<LessonCreateModel, Lesson>();
        CreateMap<LessonCreateViewModel, LessonCreateModel>();
        CreateMap<ProblemCommentCreateModel, ProblemComment>();
        CreateMap<LessonCommentCreateModel, LessonComment>();
        CreateMap<SolveCommentCreateModel, SolveComment>();
        CreateMap<LessonComment, LessonCommentModel>();
        CreateMap<ProblemComment, ProblemCommentModel>();
        CreateMap<SolveComment, SolveCommentModel>();
        CreateMap<LessonCommentModel, LessonCommentViewModel>();
        CreateMap<LessonCommentCreateViewModel, LessonCommentCreateModel>();
        CreateMap<ProblemCommentModel, ProblemCommentViewModel>();
        CreateMap<ProblemCommentCreateViewModel, ProblemCommentCreateModel>();
        CreateMap<SolveCommentModel, SolveCommentViewModel>();
        CreateMap<SolveCommentCreateViewModel, SolveCommentCreateModel>();
        CreateMap<Solve, SolveModel>();
        CreateMap<SolveCreateModel, Solve>();
        CreateMap<Language, LanguageModel>();
        CreateMap<SolveModel, SolveViewModel>();
        CreateMap<LanguageModel, LanguageViewModel>();
        CreateMap<SolveCreateViewModel, SolveCreateModel>();
    }
}
