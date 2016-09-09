using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Configuration;
using Autofac.Integration.Mvc;
using CPSS.Common.Core.Mvc.Ioc;

namespace CPSS.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var builder = AutofacContainerBuilder.CurrentContainerBuilder;
            builder.RegisterModule(new ConfigurationSettingsReader("autofac", string.Concat(AppDomain.CurrentDomain.BaseDirectory, "Config\\Ioc\\AutofacConfigBuilderTemplate.config")));
            builder.RegisterModule(new ConfigurationSettingsReader("autofac", string.Concat(AppDomain.CurrentDomain.BaseDirectory, "Config\\Ioc\\AutofacComponents.config")));
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            DependencyResolver.SetResolver(new AutofacDependencyResolver(AutofacServiceContainer.CurrentServiceContainer));


            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }
    }
}