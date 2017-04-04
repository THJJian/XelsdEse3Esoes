using System;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Features.OwnedInstances;
using CPSS.Common.Core.Authenticate;
using CPSS.Common.Core.Mvc.Filters;
using CPSS.Common.Core.Mvc.Ioc;
using CPSS.Service.ViewService.Interfaces.MenuRight;
using CPSS.Service.ViewService.ViewModels.MenuRight.Request;

namespace CPSS.Web.Controllers.Filters
{
    /// <summary>
    /// 验证用户的菜单及各种操作权限
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class OperateRightAttribute : AuthorizeAttribute
    {
        /// <summary>
        ///     需要验证的权限
        /// </summary>
        public int MenuID { set; get; }

        /// <summary>
        ///     验证成功返回true，否则返回false并执行 HandleUnauthorizedRequest函数
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            #region 如果是管理员或系统账号则不需要验证权限，本身就拥有所有权限
            
            var currentUser = CPSSAuthenticate.GetCurrentUser();
            if (currentUser.IsManager || currentUser.IsSystem) return true;

            #endregion

            var _autofacScope = AutofacServiceContainer.CurrentServiceContainer.BeginLifetimeScope(new object());
            var _service = _autofacScope.Resolve<Owned<IMenuRightCheckViewService>>();
            var request = new RequestMenuRightCheckViewModel
            {
                MenuID = this.MenuID
            };
            var model = _service.Value.CheckMenuRightByMenuID(request);

            //验证权限 有相应权限 返回true，否则返回false
            return model;
        }

        /// <summary>
        ///     在AuthorizeCore函数验证返回false时，执行该函数
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            AuthorizedRequestMethod.HandleUnauthorizedRequest(filterContext, ValidateTypeFilter.MenuValidateType,
                filter => base.HandleUnauthorizedRequest(filter));
        }
    }
}