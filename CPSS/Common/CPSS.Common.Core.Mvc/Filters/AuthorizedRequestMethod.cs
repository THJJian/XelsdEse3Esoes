using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CPSS.Common.Core.Mvc.Filters
{
    public class AuthorizedRequestMethod
    {
        public static void HandleUnauthorizedRequest(AuthorizationContext filterContext, ValidateTypeFilter validateTypeFilter, Action<AuthorizationContext> action)
        {
            var redirectUrl = string.Empty;
            switch (validateTypeFilter)
            {
                case ValidateTypeFilter.LogonValidateType:
                    var loginUrl = FormsAuthentication.LoginUrl;

                    if (loginUrl.IndexOf("{", StringComparison.OrdinalIgnoreCase) == -1)
                        action(filterContext);

                    var reg = new Regex(@"\{(\w+)\}");
                    var mc = reg.Matches(loginUrl);
                    var context = filterContext.HttpContext;
                    var returnUrl = context.Request.RawUrl.Split(new string[]{ "?" }, StringSplitOptions.RemoveEmptyEntries)[0];

                    redirectUrl = string.Concat(mc.Cast<Match>().Aggregate(loginUrl, (current, m) => current.Replace(m.Value, filterContext.RouteData.Values[m.Result("$1")].ToString())), "?returnurl=", HttpUtility.UrlEncode(returnUrl, context.Request.ContentEncoding));
                    break;
                case ValidateTypeFilter.MenuValidateType:
                    redirectUrl = "/CommonPartial/UnAuthorizedVisit";
                    break;
            }
            filterContext.Result = new RedirectResult(redirectUrl);
        }
    }
}