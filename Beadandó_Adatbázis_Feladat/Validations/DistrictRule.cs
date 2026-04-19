using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace Beadandó_Adatbázis_Feladat.Validations
{
    public class DistrictRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || value?.ToString() == "")
                return ValidationResult.ValidResult;
            if (int.TryParse(value?.ToString(), out int Num))
                if (Num > 0 && Num < 24) return ValidationResult.ValidResult;
            return new ValidationResult(false, "Nem lehet csak egész szám 0-23 között vagy nincs!");
        }
    }
}
