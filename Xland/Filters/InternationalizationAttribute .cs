using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Globalization;
using System.Threading;
using System.Web.Mvc;

namespace Xland.Filters
{
    public class InternationalizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            string language = (string)filterContext.RouteData.Values["language"] ?? "is";
            string culture = (string)filterContext.RouteData.Values["culture"] ?? "IS";

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(string.Format("{0}-{1}", language, culture));
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(string.Format("{0}-{1}", language, culture));

        }
    }
}