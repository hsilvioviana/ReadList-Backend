using AutoMapper;
using ReadList.Application.ViewModels;
using ReadList.Domain.Models;

namespace ReadList.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            #region User
            CreateMap<UserViewModel, UserModel>();
            CreateMap<SignUpViewModel, UserModel>();
            CreateMap<LoginViewModel, UserModel>();
            #endregion

            #region Book
            CreateMap<BookViewModel, BookModel>();
            CreateMap<CreateBookViewModel, BookModel>();
            #endregion

            #region BookGenreRelation
            CreateMap<BookGenreRelationViewModel, BookGenreRelationModel>();
            CreateMap<CreateBookGenreRelationViewModel, BookGenreRelationModel>();
            #endregion

            #region Genre
            CreateMap<GenreViewModel, GenreModel>();
            #endregion
        }
    }
}
