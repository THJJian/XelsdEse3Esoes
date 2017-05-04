using System.Web.Mvc;
using CPSS.Common.Core;
using CPSS.Common.Core.Mvc;
using CPSS.Common.Core.Mvc.Filters;
using CPSS.Common.Core.Type.ConstDefined;
using CPSS.Service.ViewService.Interfaces.SystemParameterConfig;
using CPSS.Service.ViewService.ViewModels.SystemParameterConfig.Request;
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

        [OperateRight(MenuID = MenuValueConstDefined.rtSystemParameter)]
        public ActionResult SysConfig()
        {
            var viewModels = this.mSystemParameterConfigViewService.GetSystemParameterConfigViewModels();
            return View(viewModels);
        }

        [HttpPost]
        [OperateRight(MenuID = MenuValueConstDefined.rtSystemParameter)]
        public JsonResult SaveSystemParameterConfig(RequestWebViewData<RequestSystemParameterConfigListViewModel> request)
        {
            var respond =this.mSystemParameterConfigViewService.SaveSystemParameterConfig(request);
            return Json(respond);
        }
    }
}