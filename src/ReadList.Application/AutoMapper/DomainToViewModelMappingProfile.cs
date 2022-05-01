using AutoMapper;
using ReadList.Application.ViewModels.User;
using ReadList.Domain.Models;

namespace ReadList.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<UserModel, UserViewModel>();
        }
    }
}
