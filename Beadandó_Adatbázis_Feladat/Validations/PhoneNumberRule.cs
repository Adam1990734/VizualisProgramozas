using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Beadandó_Adatbázis_Feladat.Validations
{
    public class PhoneNumberRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var Input = value as string;
            if (Input == null || Input.Length <= 0)
                return new ValidationResult(false, "Nem megfeleő beviteli formátum, üres!");
            if(!Regex.IsMatch(Input.Trim(), @"^\+\d{2}\d{9}$"))
                return new ValidationResult(false, "Nem meg felelő telefon formátum!");
            return ValidationResult.ValidResult;
        }
    }
}
