using Microsoft.AspNetCore.Mvc;
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
    public async Task Signup([FromQuery] SignUpViewModel viewModel)
    {
        await _service.SignUp(viewModel);
        Ok("Usuário Cadastrado");
    }

    [HttpPost("Login")]
    public async Task Login([FromQuery] LoginViewModel viewModel)
    {
        await _service.Login(viewModel);
        Ok("Usuário Logado");
    }
}
