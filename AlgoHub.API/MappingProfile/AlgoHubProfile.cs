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
        CreateMap<ProblemCreateModel, Problem>().ForMember(dest => dest.ProblemContent, opt => opt.Ignore());
        CreateMap<ProblemCreateViewModel, ProblemCreateModel>();
    }
}