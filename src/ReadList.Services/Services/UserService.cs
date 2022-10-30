using AutoMapper;
using ReadList.Domain.Interfaces;
using ReadList.Domain.Models;
using ReadList.Services.Interfaces;
using ReadList.Application.Utils;
using ReadList.Application.Validations;
using ReadList.Application.ViewModels;
using Microsoft.Extensions.Configuration;
using ReadList.Application.CustomExceptions;

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

            ThrowErrorWhen(checkUsername, "NotEqual", null, new InvalidInputException("O Username fornecido já está em uso."));

            var checkEmail = await _repository.SearchByEmail(model.Email);

            ThrowErrorWhen(checkEmail, "NotEqual", null, new InvalidInputException("O Email fornecido já está em uso."));

            model.Password = Security.Hash(model.Password);
            model.CreatedAt = DateTime.Now;
            model.UpdatedAt = DateTime.Now;

            await _repository.Create(model);

            return new AuthenticationResponse() 
            {
                Username = model.Username,
                Email = model.Email,
                Token = new JWT(_configuration).GenerateToken(_mapper.Map<UserViewModel>(model))
            };
        }

        public async Task<AuthenticationResponse> Login(LoginViewModel viewModel)
        {
            Validate(new LoginValidation(), viewModel);

            var requestUser = _mapper.Map<UserModel>(viewModel);

            var user = await _repository.SearchByUsername(requestUser.Username);

            ThrowErrorWhen(user, "Equal", null, new EntityNotFoundException("Usuário não encontrado."));

            var correctPassword = Security.Check(user.Password, requestUser.Password);

            ThrowErrorWhen(correctPassword, "Equal", false, new InvalidInputException("Senha incorreta."));

            return new AuthenticationResponse() 
            {
                Username = user.Username,
                Email = user.Email,
                Token = new JWT(_configuration).GenerateToken(_mapper.Map<UserViewModel>(user))
            };
        }

        public async Task<AuthenticationResponse> Update(string currentUsername, UpdateUserViewModel viewModel)
        {
            Validate(new UpdateUserValidation(), viewModel);

            var user = await _repository.SearchByUsername(currentUsername);

            ThrowErrorWhen(user, "Equal", null, new EntityNotFoundException("Usuário não encontrado."));

            var checkUsername = await _repository.SearchByUsername(viewModel.Username);

            if (checkUsername?.Id != user.Id)
                ThrowErrorWhen(checkUsername, "NotEqual", null, new InvalidInputException("O Username fornecido já está em uso."));

            var checkEmail = await _repository.SearchByEmail(viewModel.Email);

            if (checkEmail?.Id != user.Id)
                ThrowErrorWhen(checkEmail, "NotEqual", null, new InvalidInputException("O Email fornecido já está em uso."));

            if(!string.IsNullOrEmpty(viewModel.Password) && !string.IsNullOrEmpty(viewModel.NewPassword))
            {
                var correctPassword = Security.Check(user.Password, viewModel.Password);

                ThrowErrorWhen(correctPassword, "Equal", false, new InvalidInputException("Senha incorreta."));

                user.Password = Security.Hash(viewModel.NewPassword);
            }

            user.Username = viewModel.Username;
            user.Email = viewModel.Email;
            user.UpdatedAt = DateTime.Now;

            await _repository.Update(user);

            return new AuthenticationResponse()
            {
                Username = user.Username,
                Email = user.Email,
                Token = new JWT(_configuration).GenerateToken(_mapper.Map<UserViewModel>(user))
            };
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _repository?.Dispose();
        }
    }
}
