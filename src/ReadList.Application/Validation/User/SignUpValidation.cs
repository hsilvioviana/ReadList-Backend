using FluentValidation;
using ReadList.Application.ViewModels.User;

namespace ReadList.Application.Validation.User
{
    public class SignUpValidation : AbstractValidator<SignUpViewModel>
    {
        public SignUpValidation()
        {
            RuleFor(teste => teste.Username)
                .NotEmpty().WithMessage("{PropertyName} deve ter algum valor!")
                .MinimumLength(3).WithMessage("{PropertyName} deve ter pelo menos 3 caracteres")
                .MaximumLength(30).WithMessage("{PropertyName} deve ter no máximo 30 caracteres");

            RuleFor(teste => teste.Email)
                .NotEmpty().WithMessage("{PropertyName} deve ter algum valor!")
                .EmailAddress().WithMessage("Formato de email inválido");

            RuleFor(teste => teste.Password)
                .NotEmpty().WithMessage("{PropertyName} deve ter algum valor!")
                .MinimumLength(6).WithMessage("{PropertyName} deve ter pelo menos 6 caracteres")
                .MaximumLength(30).WithMessage("{PropertyName} deve ter no máximo 30 caracteres");
        }
    }
}
