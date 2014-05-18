using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

public static class ValidationExtensions
{
    public static bool Validate<T>(this T instance, out ICollection<ValidationResult> validationResults)
    {
        var validationContext = new ValidationContext(instance);
        validationResults = new List<ValidationResult>();
        return Validator.TryValidateObject(instance, validationContext, validationResults, true);
    }
}