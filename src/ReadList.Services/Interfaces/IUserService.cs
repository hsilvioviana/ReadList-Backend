using ReadList.Application.ViewModels;

namespace ReadList.Services.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<AuthenticationResponse> SignUp(SignUpViewModel viewModel);
        Task<AuthenticationResponse> Login(LoginViewModel viewModel);
        Task<AuthenticationResponse> Update(string currentUsername, UpdateUserViewModel viewModel);
    }
}
