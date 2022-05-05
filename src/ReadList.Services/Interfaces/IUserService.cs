using ReadList.Application.ViewModels;

namespace ReadList.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticationResponse> SignUp(SignUpViewModel viewModel);
        Task<AuthenticationResponse> Login(LoginViewModel viewModel);
    }
}
