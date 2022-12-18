using FluentValidation;
using ReadList.Application.ViewModels;

namespace ReadList.Application.Validations
{
    public class UpdateBookValidation : AbstractValidator<UpdateBookViewModel>
    {
        public UpdateBookValidation()
        {
            RuleFor(t => t.Title)
                .NotEmpty().WithMessage("{PropertyName} deve ter algum valor!")
                .MinimumLength(3).WithMessage("{PropertyName} deve ter pelo menos 3 caracteres!")
                .MaximumLength(90).WithMessage("{PropertyName} deve ter no máximo 90 caracteres!");

            RuleFor(t => t.Author)
                .NotEmpty().WithMessage("{PropertyName} deve ter algum valor!")
                .MinimumLength(3).WithMessage("{PropertyName} deve ter pelo menos 3 caracteres!")
                .MaximumLength(90).WithMessage("{PropertyName} deve ter no máximo 90 caracteres!");

            RuleFor(t => t.ReleaseYear)
                .NotEmpty().WithMessage("{PropertyName} deve ter algum valor!")
                .GreaterThan(0).WithMessage("{PropertyName} deve ter ser maior que 0!")
                .LessThan(10000).WithMessage("{PropertyName} deve ser menor que 10000!");

            RuleFor(t => t.ReadingYear)
                .NotEmpty().WithMessage("{PropertyName} deve ter algum valor!")
                .GreaterThan(0).WithMessage("{PropertyName} deve ter ser maior que 0!")
                .LessThan(10000).WithMessage("{PropertyName} deve ser menor que 10000!");

            RuleFor(t => t.IsFiction)
                .NotNull().WithMessage("{PropertyName} deve ter algum valor!");

            RuleForEach(t => t.Genres)
                .MinimumLength(3).WithMessage("Todos os gêneros devem ter pelo menos 3 caracteres!")
                .MaximumLength(30).WithMessage("Todos os gêneros devem ter no máximo 30 caracteres!");

            RuleFor(t => t.NumberOfPages)
                .NotEmpty().WithMessage("{PropertyName} deve ter algum valor!")
                .GreaterThan(0).WithMessage("{PropertyName} deve ter ser maior que 0!")
                .LessThan(100000).WithMessage("{PropertyName} deve ser menor que 100000!");

            RuleFor(t => t.CountryOfOrigin)
                .NotEmpty().WithMessage("{PropertyName} deve ter algum valor!")
                .MinimumLength(3).WithMessage("{PropertyName} deve ter pelo menos 3 caracteres!")
                .MaximumLength(30).WithMessage("{PropertyName} deve ter no máximo 30 caracteres!");

            RuleFor(t => t.Language)
                .NotEmpty().WithMessage("{PropertyName} deve ter algum valor!")
                .MinimumLength(3).WithMessage("{PropertyName} deve ter pelo menos 3 caracteres!")
                .MaximumLength(30).WithMessage("{PropertyName} deve ter no máximo 30 caracteres!");
        }
    }
}
