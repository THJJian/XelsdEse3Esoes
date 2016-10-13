using System.Web.Mvc;
using CPSS.Common.Core.Mvc;
using CPSS.Common.Core.Mvc.Filters;
using CPSS.Common.Core.Type.ConstDefined;
using CPSS.Service.ViewService.Interfaces.SystemParameterConfig;
using CPSS.Web.Controllers.Filters;

namespace CPSS.Web.Controllers
{
    [HaveToLogin]
    public class SystemParameterConfigController : WebBaseController
    {
        private readonly ISystemParameterConfigViewService mSystemParameterConfigViewService;
        public SystemParameterConfigController(ISystemParameterConfigViewService _systemParameterConfigViewService)
        {
            this.mSystemParameterConfigViewService = _systemParameterConfigViewService;
        }

        // GET: SystemParameterConfig
        [OperateRight(MenuID =  MenuValueConstDefined.rtSystemParameter)]
        public ActionResult Index()
        {
            var viewModels = this.mSystemParameterConfigViewService.GetSystemParameterConfigViewModels();
            return View(viewModels);
        }
    }
}