using AutoMapper;
using Domain.Models;
using Domain.ModelsDb;
using Domain.ViewModel;
using Domain.ViewModel.LoginAndRegistration;
using Profile = AutoMapper.Profile;

namespace Service.Model;

public class AppMappingProfile: Profile
{
    public AppMappingProfile()
    {
        CreateMap<User, UserDb>().ReverseMap();
        
        CreateMap<User, LoginViewModel>().ReverseMap();

        CreateMap<User, RegisterViewModel>().ReverseMap();
        
        CreateMap<RegisterViewModel,ConfirmEmailViewModel>().ReverseMap();
        
        CreateMap<User,ConfirmEmailViewModel>().ReverseMap();

        CreateMap<Categories, CategoriesDb>().ReverseMap();
        
        CreateMap<Categories, CategoriesViewModel>().ReverseMap();

        CreateMap<Jobs, JobsDb>().ReverseMap();
        
        CreateMap<Jobs, JobsForListOfJobsViewModel>().ReverseMap();

    }
}