using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Security;

namespace CPSS.Common.Core.Mvc.Filters
{
    public class AuthorizedRequestMethod
    {
        public static void HandleUnauthorizedRequest(AuthorizationContext filterContext,
            ValidateTypeFilter validateTypeFilter, Action<AuthorizationContext> action)
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
                    redirectUrl = mc.Cast<Match>().Aggregate(loginUrl, (current, m) => current.Replace(m.Value, filterContext.RouteData.Values[m.Result("$1")].ToString()));
                    break;
                case ValidateTypeFilter.MenuValidateType:
                    redirectUrl = "/CommonPartial/UnAuthorizedVisit";
                    break;
            }
            filterContext.Result = new RedirectResult(redirectUrl);
        }
    }
}