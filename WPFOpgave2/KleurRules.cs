using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFOpgave2
{
    public class KleurRules : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(value.ToString() == string.Empty)
            {
                return new ValidationResult(false, "Veld moet ingevuld zijn ");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
