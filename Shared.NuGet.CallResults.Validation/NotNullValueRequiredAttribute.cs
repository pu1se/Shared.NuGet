using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shared.NuGet.CallResults
{
    public class NotNullValueRequiredAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value == null)
            {
                return new ValidationResult($"This field is required", new List<string>() { validationContext.DisplayName });
            }

            return null;
        }

        private static object GetDefaultValue(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}
