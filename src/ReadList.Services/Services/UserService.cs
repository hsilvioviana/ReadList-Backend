using AutoMapper;
using ReadList.Application.Validation;
using ReadList.Application.ViewModels;
using ReadList.Domain.Interfaces;
using ReadList.Domain.Models;
using ReadList.Services.Interfaces;
using FluentValidation;

namespace ReadList.Services.Services
{
    public class UserService : IUserService
    {
        IUserRepository _repository;
        IMapper _mapper;

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

            model.CreatedAt = DateTime.Now;
            model.UpdatedAt = DateTime.Now;

            await _repository.Create(model);
        }
    }
}
