using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace RocketEshop.Controllers
{
    public class LocalizationController : Controller
    {
        private readonly RequestLocalizationOptions localizationOptions;
        private readonly List<CultureInfo> cultureList;
        
        public LocalizationController(RequestLocalizationOptions localizationOptions)
        {
            this.localizationOptions = localizationOptions;
            cultureList = localizationOptions.SupportedCultures.ToList();
        }

        [HttpPost]
        [ActionName("Change")]
        [AllowAnonymous]
        public void Change()
        {
            var currentLocalization = Request.Cookies[".AspNetCore.Culture"];
            if (currentLocalization == null)
            {
                Response.Cookies.Append(".AspNetCore.Culture", "c=en-US");
            }
            else
            {
                var currentCulture = HttpContext.Features.Get<IRequestCultureFeature>();
                var currentCultureIndex = cultureList.FindIndex(c => c.Name == currentCulture.RequestCulture.Culture.Name);
                int index = (currentCultureIndex + 1)%cultureList.Count;
                string changedCultureName = cultureList[index].Name;

                Response.Cookies.Append(".AspNetCore.Culture", $"c={changedCultureName}||uic={changedCultureName}");
            }
        }
    }
}
