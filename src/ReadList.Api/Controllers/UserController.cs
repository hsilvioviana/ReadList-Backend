using Microsoft.AspNetCore.Mvc;
using ReadList.Application.ViewModels;
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
    public async Task Signup([FromQuery] CreateUserViewModel viewModel)
    {
        await _service.SignUp(viewModel);
        Ok("Usuário Cadastrado");
    }
}
