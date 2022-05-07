using FluentValidation;
using ReadList.Application.ViewModels;

namespace ReadList.Application.Validations
{
    public class SignUpValidation : AbstractValidator<SignUpViewModel>
    {
        public SignUpValidation()
        {
            RuleFor(t => t.Username)
                .NotEmpty().WithMessage("{PropertyName} deve ter algum valor!")
                .MinimumLength(3).WithMessage("{PropertyName} deve ter pelo menos 3 caracteres!")
                .MaximumLength(30).WithMessage("{PropertyName} deve ter no máximo 30 caracteres!");

            RuleFor(t => t.Email)
                .NotEmpty().WithMessage("{PropertyName} deve ter algum valor!")
                .EmailAddress().WithMessage("Formato de email inválido!");

            RuleFor(t => t.Password)
                .NotEmpty().WithMessage("{PropertyName} deve ter algum valor!")
                .MinimumLength(6).WithMessage("{PropertyName} deve ter pelo menos 6 caracteres!")
                .MaximumLength(30).WithMessage("{PropertyName} deve ter no máximo 30 caracteres!");
        }
    }
}
