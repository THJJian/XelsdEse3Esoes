﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Autofac;
using Autofac.Configuration;
using Autofac.Integration.Mvc;
using CPSS.Common.Core;
using CPSS.Common.Core.Helper.Extension;
using CPSS.Common.Core.Mvc.Ioc;
using CPSS.Common.Core.Type;
using CPSS.Service.ViewService.Interfaces.User;
using CPSS.Service.ViewService.ViewModels.User.Request;

namespace CPSS.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var builder = AutofacContainerBuilder.CurrentContainerBuilder;
            builder.RegisterModule(new ConfigurationSettingsReader("autofac", string.Concat(AppDomain.CurrentDomain.BaseDirectory, "Config\\Ioc\\AutofacComponents.config")));
            builder.RegisterModule(new ConfigurationSettingsReader("autofac", string.Concat(AppDomain.CurrentDomain.BaseDirectory, "Config\\Ioc\\AutofacConfigBuilderTemplate.config")));
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            DependencyResolver.SetResolver(new AutofacDependencyResolver(AutofacServiceContainer.CurrentServiceContainer));

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            var httpApplication = sender as HttpApplication;
            if (httpApplication == null) return;

            #region 跳过不需要恢复身份的页面的身份恢复操作

            var _notAuthenticatePageList = new List<string> {"/verifycodeimage/index", "/signin/login", "/commonpartial/unauthorizedvisit" };
            var _request_file_path = httpApplication.Context.Request.FilePath.ToLower();
            if(_notAuthenticatePageList.Any(item=>item == _request_file_path)) return;

            #endregion

            #region 身份未丢失的不需要执行恢复身份操作

            var context = httpApplication.Context;
            var user = context.Items[BeforeCompileConstDefined.HttpContext_Login_User] as SigninUser;
            if (user != null) return;

            #endregion

            #region 设置需要使用主库连接字符串的页面

            var _useMainConnectionPageList = new List<string> { "/signin/login" };
            if (_useMainConnectionPageList.Any(item => item == _request_file_path))
                context.Items[BeforeCompileConstDefined.HttpContext_Not_Use_Main_Connection] = false;

            #endregion

            #region 身份恢复操作

            var autofac = AutofacServiceContainer.CurrentServiceContainer.BeginLifetimeScope(new object());
            var service = autofac.Resolve<ISigninUserViewService>();
            var userCookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if(userCookie == null) return;
            if (string.IsNullOrEmpty(userCookie.Value)) return;
            try
            {
                var authenticationTicket = FormsAuthentication.Decrypt(userCookie.Value);
                if (authenticationTicket == null || authenticationTicket.Expired) return;
                var userID_g = authenticationTicket.UserData;
                //if (!user.AddressIP.Equals(UserIPAddressTool.GetRealUserIPAddress())) return;
                var request = new RequestOnlineSigninUserViewModel
                {
                    SGuid = userID_g.ToGuid(),
                    AddressIP = UserIPAddressTool.GetRealUserIPAddress()
                };
                var online = service.GetOnlineSigninUserByUserID_g(request);
                if (online == null) return;
                service.FindSininUserDataModelByUserID(new RequestOnlineSigninUserViewModel
                {
                    SGuid = online.rows.SGuid,
                    UserID = online.rows.UserID
                });
            }
            catch
            {
            }
            
            #endregion
        }
    }
}