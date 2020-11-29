using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;

namespace DrinkManagerWeb.Models.ViewModels
{
    public class SwitchCultureComponents : ViewComponent
    {
        private readonly IOptions<RequestLocalizationOptions> _localizationOptions;

        public SwitchCultureComponents(IOptions<RequestLocalizationOptions> localizationOptions)
        {
            _localizationOptions = localizationOptions;
        }

        public IViewComponentResult Invoke()
        {
            var cultureFeature = HttpContext.Features.Get<IRequestCultureFeature>();
            var model = new SwitchCulture
            {
                SupportedCultures = _localizationOptions.Value.SupportedCultures.ToList(),
                CurrentUiCulture = cultureFeature.RequestCulture.UICulture
            };

            return View(model);
        }
    }
}
