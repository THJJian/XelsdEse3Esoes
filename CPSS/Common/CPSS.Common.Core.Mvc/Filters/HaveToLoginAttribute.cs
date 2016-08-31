using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CPSS.Common.Core.Mvc.Filters
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class HaveToLoginAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if(httpContext == null) throw  new Exception("服务器异常(httpContext)");
            var user = httpContext.User;
            var loginUser = httpContext.Items["__LoginUser"];
            return user.Identity.IsAuthenticated && loginUser.UserID > 0;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            AuthorizedRequestMethod.HandleUnauthorizedRequest(filterContext, filter => base.HandleUnauthorizedRequest(filter));
        }
    }
}