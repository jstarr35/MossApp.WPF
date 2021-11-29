using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MossApp.Utilities.Validation
{
    public class IsNumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return long.TryParse(value.ToString(), out _) 
                ? ValidationResult.ValidResult 
                : new ValidationResult(false, "Value must be numeric");
        }
    }
}
