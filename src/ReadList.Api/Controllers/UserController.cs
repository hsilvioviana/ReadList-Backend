using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadList.Application.ViewModels.Authentication;
using ReadList.Application.ViewModels.User;
using ReadList.Services.Interfaces;

namespace ReadList.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    IUserService _service;
    public UserController (IUserService service)
    {
        _service = service;
    }

    [HttpPost("SignUp")]
    [AllowAnonymous]
    public async Task<AuthenticationResponse> Signup([FromQuery] SignUpViewModel viewModel)
    {
        return await _service.SignUp(viewModel);
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<AuthenticationResponse> Login([FromQuery] LoginViewModel viewModel)
    {
        return await _service.Login(viewModel);
    }
}
