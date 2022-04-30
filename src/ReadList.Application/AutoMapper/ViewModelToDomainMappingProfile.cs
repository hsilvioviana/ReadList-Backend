using AutoMapper;
using ReadList.Application.ViewModels.User;
using ReadList.Domain.Models;

namespace ReadList.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<UserViewModel, UserModel>();
            CreateMap<SignUpViewModel, UserModel>();
            CreateMap<LoginViewModel, UserModel>();
        }
    }
}
