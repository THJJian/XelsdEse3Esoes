using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Security;

namespace CPSS.Common.Core.Mvc.Filters
{
    public class AuthorizedRequestMethod
    {
        public static void HandleUnauthorizedRequest(AuthorizationContext filterContext, Action<AuthorizationContext> action)
        {
            var loginUrl = FormsAuthentication.LoginUrl;

            if (loginUrl.IndexOf("{", StringComparison.OrdinalIgnoreCase) == -1)
            {
                action(filterContext);
            }

            var reg = new Regex(@"\{(\w+)\}");
            var mc = reg.Matches(loginUrl);
            loginUrl = mc.Cast<Match>().Aggregate(loginUrl, (current, m) => current.Replace(m.Value, filterContext.RouteData.Values[m.Result("$1")].ToString()));
            filterContext.Result = new RedirectResult(loginUrl);
        }
    }
}