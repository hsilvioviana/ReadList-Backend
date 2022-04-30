using AutoMapper;
using ReadList.Domain.Interfaces;
using ReadList.Domain.Models;
using ReadList.Services.Interfaces;
using FluentValidation;
using ReadList.Application.Utils;
using ReadList.Application.Validation.User;
using ReadList.Application.ViewModels.User;

namespace ReadList.Services.Services
{
    public class UserService : IUserService
    {
        protected readonly IUserRepository _repository;
        protected readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task SignUp(SignUpViewModel viewModel)
        {
            var validation = new SignUpValidation();

            validation.Validate(viewModel, options => options.ThrowOnFailures());

            var model = _mapper.Map<UserModel>(viewModel);

            model.Password = Security.Hash(model.Password);
            model.CreatedAt = DateTime.Now;
            model.UpdatedAt = DateTime.Now;

            await _repository.Create(model);
        }

        public async Task Login(LoginViewModel viewModel)
        {
            var validation = new LoginValidation();

            validation.Validate(viewModel, options => options.ThrowOnFailures());

            var model = _mapper.Map<UserModel>(viewModel);

            var user = await _repository.SearchByUsername(model.Username);

            if (user.Username == "")
            {
                throw new Exception("Usuário não encontrado.");
            }

            var correctPassword = Security.Check(user.Password, model.Password);

            if (!correctPassword)
            {
                throw new Exception("Senha incorreta.");
            }
        }
    }
}
