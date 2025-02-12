using System.ComponentModel.DataAnnotations;

namespace APIRequest.Validations
{
    public class PrimeiraLetraMaisculaAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var primeriaLetra = value.ToString()[0].ToString();
            if (primeriaLetra != primeriaLetra.ToUpper())
            {
                return new ValidationResult("A primeira letra do nome deve ser maiúscula");
            }
            return ValidationResult.Success;
        }
    }
}
