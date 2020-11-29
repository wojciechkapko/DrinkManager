using System.Collections.Generic;
using System.Globalization;

namespace DrinkManagerWeb.Models
{
    public class SwitchCulture
    {
        public CultureInfo CurrentUiCulture { get; set; }
        public List<CultureInfo> SupportedCultures { get; set; }
    }
}
