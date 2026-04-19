using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace Beadandó_Adatbázis_Feladat.Validations
{
    public sealed class OnlyNumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(double.TryParse(value?.ToString(), out double Num))
                if (Num >= 0) return ValidationResult.ValidResult;
            return new ValidationResult(false, "A megadott beviteli mezőben csak szám lehet!");
        }
    }
}
