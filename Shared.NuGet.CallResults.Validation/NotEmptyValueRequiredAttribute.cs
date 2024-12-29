using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shared.NuGet.CallResults
{
    // rename to NotDefaultAndNotNullValueRequiredAttribute.
    public class NotEmptyValueRequiredAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value == null)
            {
                return new ValidationResult($"This field is required", new List<string>() { validationContext.DisplayName });
            }

            var type = value.GetType();
            if (!type.IsValueType)
            {
                return null;
            }

            if (Equals(value, GetDefaultValue(type)))
            {
                return new ValidationResult($"This field must be in valid format and not equal to default type value", new List<string>() { validationContext.DisplayName });
            }
            return null;
        }

        private static object GetDefaultValue(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}
