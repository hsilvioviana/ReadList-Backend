using ReadList.Application.ViewModels.User;

namespace ReadList.Services.Interfaces
{
    public interface IUserService
    {
        Task SignUp(SignUpViewModel viewModel);
        Task Login(LoginViewModel viewModel);
    }
}
