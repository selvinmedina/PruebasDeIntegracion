using System.ComponentModel.DataAnnotations;

namespace AcademiaFarsiman.Api.Common.Attributes
{
    public class CampoRequeridoAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            bool esValido = true;

            if (value is null)
            {
                esValido = false;
            }
            else if (value is int || value is int?)
            {
                var valor = int.Parse(value?.ToString() ?? "");

                if (valor == default)
                {
                    esValido = false;
                }
            }

            if (!esValido)
            {
                throw new ArgumentException($"El campo {validationContext.DisplayName} es requerido.");
            }

            return ValidationResult.Success;
        }
    }
}

