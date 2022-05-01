using AutoMapper;
using ReadList.Domain.Interfaces;
using ReadList.Domain.Models;
using ReadList.Services.Interfaces;
using ReadList.Application.Utils;
using ReadList.Application.Validation.User;
using ReadList.Application.ViewModels.User;
using ReadList.Application.ViewModels.Authentication;
using Microsoft.Extensions.Configuration;

namespace ReadList.Services.Services
{
    public class UserService : BaseService, IUserService
    {
        protected readonly IConfiguration _configuration;
        protected readonly IUserRepository _repository;
        protected readonly IMapper _mapper;

        public UserService(IConfiguration configuration, IUserRepository repository, IMapper mapper)
        {
            _configuration = configuration;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> SignUp(SignUpViewModel viewModel)
        {
            Validate(new SignUpValidation(), viewModel);

            var model = _mapper.Map<UserModel>(viewModel);

            var checkUsername = await _repository.SearchByUsername(model.Username);

            CheckVariable(checkUsername.Username, "Equal", "", "O Username fornecido já está em uso.");

            var checkEmail = await _repository.SearchByEmail(model.Email);

            CheckVariable(checkEmail.Email, "Equal", "", "O Email fornecido já está em uso.");

            model.Password = Security.Hash(model.Password);
            model.CreatedAt = DateTime.Now;
            model.UpdatedAt = DateTime.Now;

            await _repository.Create(model);

            return new AuthenticationResponse() 
            {
                Username = model.Username,
                Token = new JWT(_configuration).GenerateToken(_mapper.Map<UserViewModel>(model))
            };
        }

        public async Task<AuthenticationResponse> Login(LoginViewModel viewModel)
        {
            Validate(new LoginValidation(), viewModel);

            var requestUser = _mapper.Map<UserModel>(viewModel);

            var user = await _repository.SearchByUsername(requestUser.Username);

            CheckVariable(user.Username, "NotEqual", "", "Usuário não encontrado.");

            var correctPassword = Security.Check(user.Password, requestUser.Password);

            CheckVariable(correctPassword, "Equal", true, "Senha incorreta.");

            return new AuthenticationResponse() 
            {
                Username = user.Username,
                Token = new JWT(_configuration).GenerateToken(_mapper.Map<UserViewModel>(user))
            };
        }
    }
}
