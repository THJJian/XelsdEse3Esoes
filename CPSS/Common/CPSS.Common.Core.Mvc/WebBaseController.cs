using System.Web.Routing;

namespace CPSS.Common.Core.Mvc
{
    public class WebBaseController : RazorBaseController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            this.WorkContext.Resource
                .AddPageCss("easyui-1.5/themes/default/easyui", "easyui-1.5/themes/icon")
                .AddPageScripts("jquery-1.11.3.min", "easyui-1.5/jquery.easyui.min", "cspplib", "printer", "controlshelper");
        }
    }
}