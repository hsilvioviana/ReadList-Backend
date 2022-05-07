using AutoMapper;
using ReadList.Application.ViewModels;
using ReadList.Domain.Models;

namespace ReadList.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            #region User
            CreateMap<UserModel, UserViewModel>();
            #endregion

            #region Book
            CreateMap<BookModel, BookViewModel>()
                .ForMember(vm => vm.Genres, opt => opt.MapFrom(m => m.BookGenreRelations.Select(bgr => bgr.Genre.Name)));
            #endregion

            #region Genre
            CreateMap<GenreModel, GenreViewModel>();
            #endregion
        }
    }
}
