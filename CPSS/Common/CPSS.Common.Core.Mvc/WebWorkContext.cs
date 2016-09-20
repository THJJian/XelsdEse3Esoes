using Autofac;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Autofac.Features.OwnedInstances;
using CPSS.Common.Core.Helper.WebConfig;
using CPSS.Common.Core.Mvc.Ioc;

namespace CPSS.Common.Core.Mvc
{
    public class WebWorkContext
    {

        private readonly Dictionary<string, object> _dicContainers = new Dictionary<string, object>();

        private ILifetimeScope _autofacScope;

        public Resource Resource;

        public CurrentUser CurrentUser = new CurrentUser();

        public WebWorkContext(RequestContext requestContext)
        {
            this.CurrentRequestContext = requestContext;
            this.Resource = new Resource();
        }

        public RequestContext CurrentRequestContext { get; set; }

        private event ContainerDisposeHandler OnDisposeContainer;

        //[Obsolete("MVC 中尽量不要使用此方法了")]
        public string GetQueryString(string key, string defVal = "")
        {
            var result = this.CurrentRequestContext.HttpContext.Request[key];
            return result == null ? defVal : result.ToString(CultureInfo.InvariantCulture);
        }

        public string GetQueryStringDecode(string key, string defVal = "", bool decode = true)
        {
            var result = this.CurrentRequestContext.HttpContext.Request[key];           
            if (string.IsNullOrEmpty(result)) return defVal;
            if (decode) result = HttpUtility.UrlDecode(HttpUtility.UrlDecode(result));
            return result != null ? result.ToString(CultureInfo.InvariantCulture) : defVal;
        }

        public string GetRouterString(string key, string defVal = "")
        {
            var result = this.CurrentRequestContext.RouteData.Values[key];
            return result == null ? defVal : result.ToString();
        }

        public int GetRouterInt(string key, int defVal = 0)
        {
            var result = GetRouterString(key);
            if (string.IsNullOrEmpty(result)) return defVal;
            int val;
            return int.TryParse(result, out val) ? val : defVal;
        }

        public int GetQueryInt(string key, int defVal = 0)
        {
            var result = GetQueryString(key);
            if (string.IsNullOrEmpty(result)) return defVal;
            int val;
            return int.TryParse(result, out val) ? val : defVal;
        }

        public T CreateInstance<T>()
        {
            Owned<T> service;
            var key = typeof(T).FullName;
            object oservice;
            if (this._dicContainers.TryGetValue(key, out oservice))
            {
                service = oservice as Owned<T>;
            }
            else
            {
                if (this._autofacScope == null)
                {
                    this._autofacScope = AutofacServiceContainer.CurrentServiceContainer.BeginLifetimeScope(new Object());
                }
                service = this._autofacScope.Resolve<Owned<T>>();
                this._dicContainers.Add(key, service);
                this.OnDisposeContainer += service.Dispose;
            }

            if (service != null)
            {
                return service.Value;
            }

            throw new Exception();
        }

        public void LifetimeScopeHandler<T>(Action<T> action)
        {
            action(this.CreateInstance<T>());
        }

        public void Dispose()
        {
            try
            {
                if (this.OnDisposeContainer != null)
                {
                    this.OnDisposeContainer();
                }
                if (this._autofacScope != null)
                {
                    this._autofacScope.Dispose();
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        private delegate void ContainerDisposeHandler();
    }
    /// <summary>
    /// 当前User
    /// </summary>
    public class CurrentUser
    {
        public CurrentUser()
        {
            this.UserName = string.Empty;
            this.PerID = 0;
            this.UserID = 0;
            this.SMSMobile = string.Empty;
        }

        public string UserName { get; set; }
        public int PerID { get; set; }
        public int UserID { get; set; }
        public string SMSMobile { get; set; }
    }

    public class Resource
    {
        public Resource()
        {
            this.Css = new List<string>();
            this.JavaScript = new List<string>();
        }

        public List<string> JavaScript { get; set; }

        public List<string> Css { get; set; }

        /// <summary>
        /// 添加页面执行的JS脚本。注意：被依赖的JS请写在依赖的JS的后面。如：AddPageScripts("CPSSLibJQ", "jquery-1.11.1")
        /// </summary>
        /// <param name="urls"></param>
        /// <returns></returns>
        public Resource AddPageScripts(params string[] urls)
        {
            if (urls == null) return this;
            if (this.JavaScript == null)
            {
                this.JavaScript = new List<string>();
            }
            foreach (var realurl in urls.Select(url => string.Concat(WebConfigHelper.ResourceDomain(), "/Scripts/",url, ".js")).Where(realurl => !this.JavaScript.Exists(p => p.Equals(realurl, StringComparison.CurrentCultureIgnoreCase))))
            {
                this.JavaScript.Add(realurl);
            }
            return this;
        }

        public Resource AddPageCss(params string[] urls)
        {
            if (this.Css == null)
            {
                this.Css = new List<string>();
            }
            foreach (var realurl in urls.Select(url => string.Concat(WebConfigHelper.ResourceDomain(), "/Css/", url, ".css")).Where(realurl => !this.Css.Exists(p => p.Equals(realurl, StringComparison.CurrentCultureIgnoreCase))))
            {
                this.Css.Add(realurl);
            }
            return this;
        }
    }
}