using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MossApp.Utilities.Validation
{
    public class IntegerValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            bool result = long.TryParse((value ?? "").ToString(), out long intVal);
            return (!result || intVal <= 0)
                ? new ValidationResult(false, "Value must be greater than 0.")
                : ValidationResult.ValidResult;
        }
    }
}
