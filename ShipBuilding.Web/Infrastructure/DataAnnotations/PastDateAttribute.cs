using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ShipBuilding.Web.Infrastructure.DataAnnotations
{
    public class PastDateAttribute : ValidationAttribute, IClientModelValidator
    {
        protected override ValidationResult? IsValid(object? value,
            ValidationContext validationContext)
        {

            if (value is DateTime date && date > DateTime.Now)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            // Додаємо атрибут, який JS зможе прочитати
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-pastdate",
                ErrorMessage ?? "Дата не може бути в майбутньому");
        }

    }
}
