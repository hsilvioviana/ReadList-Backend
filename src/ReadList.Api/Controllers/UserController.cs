using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadList.Application.ViewModels;
using ReadList.Services.Interfaces;

namespace ReadList.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        protected readonly IUserService _service;

        public UserController (IUserService service)
        {
            _service = service;
        }

        // POST api/users/signup
        /// <summary>
        /// Cadastro de usuário.
        /// </summary>
        /// <remarks>
        /// Exemplo de body:
        /// 
        ///     {
        ///         "username": "joao123",
        ///         "email": "joao123@email.com",
        ///         "password": "123456"
        ///     }
        /// </remarks>
        /// <param name="viewModel">Dados de criação</param>
        /// <response code="200">Cadastro realizado com sucesso.</response>
        /// <response code="400">Erro no cadastro.</response>
        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<AuthenticationResponse> Signup([FromBody] SignUpViewModel viewModel)
        {
            return await _service.SignUp(viewModel);
        }


        // POST api/users/login
        /// <summary>
        /// Login de usuário.
        /// </summary>
        /// <remarks>
        /// Exemplo de body:
        /// 
        ///     {
        ///         "username": "joao123",
        ///         "password": "123456"
        ///     }
        /// </remarks>
        /// <param name="viewModel">Dados de login.</param>
        /// <response code="200">Login realizado com sucesso.</response>
        /// <response code="400">Erro no login.</response>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<AuthenticationResponse> Login([FromBody] LoginViewModel viewModel)
        {
            return await _service.Login(viewModel);
        }
    }
}
