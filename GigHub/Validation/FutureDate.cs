using GigHub.Dtos;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace GigHub.Validation
{
    public class FutureDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var gigDto = (GigDto)validationContext.ObjectInstance;

            var isValid = DateTime.TryParseExact(
                Convert.ToString(gigDto.Date),
                "d MMM yyyy",
                CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out var dateTime);

            return isValid && dateTime > DateTime.Now
                ? ValidationResult.Success
                : new ValidationResult($"{GetDisplayName()} should be in format 'd MMM yyyy' and points to future date.");

        }

        private string GetDisplayName()
        {
            const string defaultValidationMsg = "Date field";
            try
            {
                var property = typeof(GigDto).GetProperty("Date");
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