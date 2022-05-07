using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadList.Application.ViewModels;
using ReadList.Services.Interfaces;

namespace ReadList.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    IUserService _service;
    public UserController (IUserService service)
    {
        _service = service;
    }

    [HttpPost("signup")]
    [AllowAnonymous]
    public async Task<AuthenticationResponse> Signup([FromQuery] SignUpViewModel viewModel)
    {
        return await _service.SignUp(viewModel);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<AuthenticationResponse> Login([FromQuery] LoginViewModel viewModel)
    {
        return await _service.Login(viewModel);
    }
}
