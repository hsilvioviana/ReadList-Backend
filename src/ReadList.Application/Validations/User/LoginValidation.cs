using FluentValidation;
using ReadList.Application.ViewModels;

namespace ReadList.Application.Validations
{
    public class LoginValidation : AbstractValidator<LoginViewModel>
    {
        public LoginValidation()
        {
            RuleFor(teste => teste.Username)
                .NotEmpty().WithMessage("{PropertyName} deve ter algum valor!")
                .MinimumLength(3).WithMessage("{PropertyName} deve ter pelo menos 3 caracteres")
                .MaximumLength(30).WithMessage("{PropertyName} deve ter no máximo 30 caracteres");

            RuleFor(teste => teste.Password)
                .NotEmpty().WithMessage("{PropertyName} deve ter algum valor!")
                .MinimumLength(6).WithMessage("{PropertyName} deve ter pelo menos 6 caracteres")
                .MaximumLength(30).WithMessage("{PropertyName} deve ter no máximo 30 caracteres");
        }
    }
}
