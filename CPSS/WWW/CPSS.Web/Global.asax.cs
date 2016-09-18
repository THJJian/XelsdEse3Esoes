using System;
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
            var context = (sender as HttpApplication).Context;
            var user = context.Items["__Login__User__"] as SigninUser;
            if (user != null) return;

            var autofac = AutofacServiceContainer.CurrentServiceContainer.BeginLifetimeScope(new object());
            var service = autofac.Resolve<ISigninUserViewService>();
            HttpCookie userCookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (string.IsNullOrEmpty(userCookie?.Value)) return;
            try
            {
                var authenticationTicket = FormsAuthentication.Decrypt(userCookie.Value);
                if (authenticationTicket == null || authenticationTicket.Expired) return;
                var userID_g = authenticationTicket.UserData;
                if (!user.AddressIP.Equals(UserIPAddressTool.GetRealUserIPAddress())) return;
                var request = new RequestOnlineSigninUserViewModel
                {
                    SGuid = userID_g.ToGuid()
                };
                var online = service.GetOnlineSigninUserByUserID_g(request);
                if (online == null) return;
                service.FindSininUserDataModelByUserID(new RequestOnlineSigninUserViewModel
                {
                    SGuid = online.Data.SGuid,
                    UserID = online.Data.UserID
                });
            }
            catch
            {
            }
        }
    }
}