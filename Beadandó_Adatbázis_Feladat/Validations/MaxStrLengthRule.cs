using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace Beadandó_Adatbázis_Feladat.Validations
{
    public class MaxStrLengthRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var Input = value as string;
            if (Input == null)
                return new ValidationResult(false, "Nem megfelő bevitel, üres!");
            if (Input.Length <= 25 && Input.Length > 0)
                    return ValidationResult.ValidResult;
            return new ValidationResult(false, "A megadott mezőnek a mérete maximum 25 lehet és minimum 1");
        }
    }
}
