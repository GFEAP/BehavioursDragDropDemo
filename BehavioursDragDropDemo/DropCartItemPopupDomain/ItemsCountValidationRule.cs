using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace BehavioursDragDropDemo.DropCartItemPopupDomain
{
    /// <summary>
    /// </summary>
    public class ItemsCountValidationRule : ValidationRule
    {
        public ItemsCountValidationRule()
        {}

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string s && s.Length > 0 && int.TryParse(s, out int i))
                    return ValidationResult.ValidResult;
            else
                return new ValidationResult(false, "Not a number");
        }
    }
}
