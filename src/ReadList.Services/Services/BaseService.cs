using FluentValidation;

namespace ReadList.Services.Services
{
    public class BaseService
    {
        public void Validate<TV, TM>(TV validation, TM model) where TV :  AbstractValidator<TM>
        {
            var result = validation.Validate(model);

            if (!result.IsValid)
            {
                throw new Exception(result.Errors[0].ToString());
            }
        }

        public void CheckVariable<T>(T variable, string TypeOfComparison, T expectedValue, string errorMessage)
        {
            var passed = false;

            if (TypeOfComparison == "Equal")
            {
                passed = EqualityComparer<T>.Default.Equals(variable, expectedValue);
            }
            else if (TypeOfComparison == "NotEqual")
            {
                passed = !EqualityComparer<T>.Default.Equals(variable, expectedValue);
            }

            if (!passed)
            {
                throw new Exception(errorMessage);
            }
        }
    }
}
