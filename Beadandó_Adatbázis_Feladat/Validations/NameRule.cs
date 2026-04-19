using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Beadandó_Adatbázis_Feladat.Validations
{
    public class NameRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var Input = value as string;
            if (Input == null || Input.Length <= 0)
                return new ValidationResult(false, "Nem megfeleő beviteli formátum, üres!");
            if (!Regex.IsMatch(Input.Trim(), @"^[A-ZÁÉÍÓÖŐÚÜŰ][a-záéíóöőúüű]+( [A-ZÁÉÍÓÖŐÚÜŰ][a-záéíóöőúüű]+)+$"))
                return new ValidationResult(false, "Nem megfelelő név formátum!");
            return ValidationResult.ValidResult;
        }
    }
}
