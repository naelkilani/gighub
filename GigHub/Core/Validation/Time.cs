using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using GigHub.Core.Dtos;

namespace GigHub.Core.Validation
{
    public class Time : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var isValid = DateTime.TryParseExact(
                Convert.ToString(value),
                "HH:mm",
                CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out _);

            return isValid
                ? ValidationResult.Success
                : new ValidationResult($"{GetDisplayName()} should be in format 'HH:mm'.");

        }

        private string GetDisplayName()
        {
            const string defaultValidationMsg = "Time field";
            try
            {
                var property = typeof(GigDto).GetProperty("Time");
                if (property == null)
                    return defaultValidationMsg;

                var attribute = (DisplayAttribute)property.GetCustomAttributes(typeof(DisplayAttribute), true).FirstOrDefault();

                return attribute == null ? defaultValidationMsg : attribute.Name;
            }
            catch (Exception)
            {
                return defaultValidationMsg;
            }
        }
    }
}