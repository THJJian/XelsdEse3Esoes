using System.Web.Routing;

namespace CPSS.Common.Core.Mvc
{
    public class WebBaseController : RazorBaseController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            this.WorkContext.Resource.AddPageScripts("jquery", "cspplib");
        }
    }
}