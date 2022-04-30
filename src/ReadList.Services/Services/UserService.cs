using AutoMapper;
using ReadList.Application.Validation;
using ReadList.Application.ViewModels;
using ReadList.Domain.Interfaces;
using ReadList.Domain.Models;
using ReadList.Services.Interfaces;
using FluentValidation;
using ReadList.Application.Utils;

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

        public async Task SignUp(CreateUserViewModel viewModel)
        {
            var validation = new CreateUserValidation();

            validation.Validate(viewModel, options => options.ThrowOnFailures());

            var model = _mapper.Map<UserModel>(viewModel);

            model.Password = Security.Hash(model.Password);
            model.CreatedAt = DateTime.Now;
            model.UpdatedAt = DateTime.Now;

            await _repository.Create(model);
        }
    }
}
