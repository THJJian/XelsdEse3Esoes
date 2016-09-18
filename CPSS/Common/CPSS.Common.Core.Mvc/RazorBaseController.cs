using System.Web.Mvc;
using System.Web.Routing;

namespace CPSS.Common.Core.Mvc
{
    public abstract class RazorBaseController : Controller, IRazorController
    {
        public WebWorkContext WorkContext { private set; get; }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            WorkContext = new WebWorkContext(requestContext);
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
            if (!filterContext.IsChildAction) WorkContext?.Dispose();
        }
    }
}