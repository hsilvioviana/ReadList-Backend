using FluentValidation;
using ReadList.Application.ViewModels;

namespace ReadList.Application.Validation
{
    public class CriarTesteFluxoValidation : AbstractValidator<CriarTesteFluxoViewModel>
    {
        public CriarTesteFluxoValidation()
        {
            RuleFor(teste => teste.Nome).NotEmpty().WithMessage("{PropertyName} deve ter algum valor!");

            RuleFor(teste => teste.Numero).GreaterThanOrEqualTo(18).WithMessage("Apenas numeros maiores que 18 vão ser aceitos nesse teste!");
        }
    }
}
