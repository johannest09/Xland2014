using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Xland.Helpers
{
    public static class HTMLHelpers
    {
        public static MvcHtmlString LabelDisplayFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            StringBuilder html = new StringBuilder();
            string disp = helper.DisplayFor(expression).ToString();
            if (!string.IsNullOrWhiteSpace(disp))
            {
                //html.AppendLine(helper.DisplayNameFor(expression).ToString());
                //html.AppendLine(disp);

                html.AppendLine("<dt>");
                html.AppendLine(helper.DisplayNameFor(expression).ToString());
                html.AppendLine("</dt>");
                html.AppendLine("<dd>");
                html.AppendLine(disp);
                html.AppendLine("</dd>");
            }
            return MvcHtmlString.Create(html.ToString());
        }
    }
}