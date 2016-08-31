using System;
using System.Web;
using System.Web.Mvc;

namespace CPSS.Common.Core.Mvc.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class OperateRightAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if(string.IsNullOrEmpty(this.OperateRightIDs)) return true;

            var _rightIDs = this.OperateRightIDs.Split(',');
            //TODO 验证权限 有相应权限 返回true，否则返回false
            System.Console.Write(_rightIDs[0]);
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            AuthorizedRequestMethod.HandleUnauthorizedRequest(filterContext, filter => base.HandleUnauthorizedRequest(filter));
        }

        /// <summary>
        /// 需要验证的权限
        /// </summary>
        public string OperateRightIDs { set; get; }
    }
}