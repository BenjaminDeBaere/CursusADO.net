using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFOpgave2
{
    public class PrijsRules : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            decimal prijs;
            NumberStyles style = NumberStyles.Currency;
            if(Decimal.TryParse(value.ToString(),style, cultureInfo, out prijs))
            {
                if(prijs<=0)
                {
                    return new ValidationResult(false, "Gelieve een getal in te vullen groter dan €0");
                }
                else
                {
                    return ValidationResult.ValidResult;
                }

            }
            else
            {
                return new ValidationResult(false, "Gelieve een getal in te vullen");
            }
        }
    }
}
