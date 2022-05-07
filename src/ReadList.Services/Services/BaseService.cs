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

        public void ThrowErrorWhen<T>(T value1, string TypeOfComparison, T value2, string errorMessage)
        {
            var throwError = false;

            if (TypeOfComparison == "Equal")
            {
                throwError = EqualityComparer<T>.Default.Equals(value1, value2);
            }
            else if (TypeOfComparison == "NotEqual")
            {
                throwError = !EqualityComparer<T>.Default.Equals(value1, value2);
            }

            if (throwError)
            {
                throw new Exception(errorMessage);
            }
        }
    }
}
