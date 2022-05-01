using ReadList.Application.ViewModels.Authentication;
using ReadList.Application.ViewModels.User;

namespace ReadList.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticationResponse> SignUp(SignUpViewModel viewModel);
        Task<AuthenticationResponse> Login(LoginViewModel viewModel);
    }
}
