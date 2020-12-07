using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DrinkManagerWeb.ViewComponents
{
    public class CulturePicker : ViewComponent
    {
        private readonly IOptions<RequestLocalizationOptions> _localizationOptions;

        public CulturePicker(IOptions<RequestLocalizationOptions> localizationOptions)
        {
            _localizationOptions = localizationOptions;
        }

        public IViewComponentResult Invoke()
        {
            var cultureFeature = HttpContext.Features.Get<IRequestCultureFeature>();
            var model = new CulturePickerModel
            {
                SupportedCultures = _localizationOptions.Value.SupportedCultures.ToList(),
                CurrentUiCulture = cultureFeature.RequestCulture.UICulture,
                QueryString = $"{this.HttpContext.Request.QueryString}"
            };


            //TODO: Remove additional ? signs from the string.
            // TODO 2: Remove culture part from this string every time since its added manually in Default.cshtml

           // model.QueryString = model.QueryString.Replace("");

            return View(model);
        }
    }

    public class CulturePickerModel
    {
        public CultureInfo CurrentUiCulture { get; set; }
        public List<CultureInfo> SupportedCultures { get; set; }
        public string QueryString { get; set; }
    }
}
