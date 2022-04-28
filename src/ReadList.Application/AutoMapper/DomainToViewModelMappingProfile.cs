using AutoMapper;
using ReadList.Application.ViewModels;
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
