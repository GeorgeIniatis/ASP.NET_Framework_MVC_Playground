using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Threading;

namespace ASP.NET_Framework_MVC_Playground.Controllers
{
    public class BaseController : Controller
    {
        public ActionResult ChangeCulture(string cultureChosen)
        {
            string referer = HttpContext.Request.Headers["Referer"].ToString();
            string[] urlParts = referer.Split('/');
            urlParts[3] = cultureChosen;

            return Redirect(string.Join("/", urlParts));
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string culture = filterContext.RouteData.Values["culture"]?.ToString()?? "en-GB";

            // Set the action parameter just in case we didn't get one
            // from the route.
            filterContext.ActionParameters["culture"] = culture;

            var cultureInfo = CultureInfo.GetCultureInfo(culture);

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureInfo.TwoLetterISOLanguageName);

            // Because we've overwritten the ActionParameters, we
            // make sure we provide the override to the 
            // base implementation.
            base.OnActionExecuting(filterContext);
        }

        public ActionResult RedirectToLocalized()
        {
            return RedirectPermanent("/en-GB");
        }
    }
}