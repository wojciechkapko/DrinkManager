using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DrinkManager.API.ViewComponents
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
                CurrentUiCulture = cultureFeature.RequestCulture.UICulture
            };

            return View(model);
        }
    }

    public class CulturePickerModel
    {
        public CultureInfo CurrentUiCulture { get; set; }
        public List<CultureInfo> SupportedCultures { get; set; }
    }
}
