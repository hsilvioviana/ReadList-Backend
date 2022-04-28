using ReadList.Application.ViewModels;

namespace ReadList.Services.Interfaces
{
    public interface IUserService
    {
        Task SignUp(CreateUserViewModel viewModel);
    }
}
