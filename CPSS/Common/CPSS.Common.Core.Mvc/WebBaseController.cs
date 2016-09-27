using System.Web.Routing;

namespace CPSS.Common.Core.Mvc
{
    public class WebBaseController : RazorBaseController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            this.WorkContext.Resource
                .AddPageCss("ligerUI-1.3.3/skins/Aqua/css/ligerui-all", "ligerUI-1.3.3/skins/ligerui-icons")
                .AddPageScripts("jquery-1.11.3.min", "ligerUI-1.3.3/js/core/base", "cspplib");
        }
    }
}