using System.Web.Mvc;

namespace CPSS.Common.Core.Mvc
{
    public abstract class RazorWebViewPage<TModel> : WebViewPage<TModel>
    {
        public WebWorkContext WorkContext;

        public sealed override void InitHelpers()
        {
            base.InitHelpers();
            this.WorkContext = ((IRazorController)(this.ViewContext.Controller)).WorkContext;
        }

        public sealed override void Write(object value)
        {
            base.Write(value);
        }
    }

    /// <summary>
    /// PC前台视图页面基类型
    /// </summary>
    public abstract class RazorWebViewPage : RazorWebViewPage<dynamic>
    {
    }

}