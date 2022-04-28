using AutoMapper;
using ReadList.Application.ViewModels;
using ReadList.Domain.Models;

namespace ReadList.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<UserViewModel, UserModel>();
            CreateMap<CreateUserViewModel, UserModel>();
        }
    }
}
