using System;
using System.Web;
using System.Web.Mvc;

namespace CPSS.Common.Core.Mvc.Filters
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class HaveToLoginAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null) throw new Exception("服务器异常(httpContext)");
            var user = httpContext.Items["__Login__User__"] as SigninUser;
            return user?.UserID > 0;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            AuthorizedRequestMethod.HandleUnauthorizedRequest(filterContext, ValidateTypeFilter.LogonValidateType,  filter => base.HandleUnauthorizedRequest(filter));
        }
    }
}