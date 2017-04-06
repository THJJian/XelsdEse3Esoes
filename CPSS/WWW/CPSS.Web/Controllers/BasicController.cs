using System.Collections.Generic;
using System.Web.Mvc;
using CPSS.Common.Core;
using CPSS.Common.Core.Mvc;
using CPSS.Common.Core.Mvc.Filters;
using CPSS.Common.Core.Type.ConstDefined;
using CPSS.Service.ViewService.Interfaces.Basic;
using CPSS.Service.ViewService.ViewModels.SubCompany.Request;
using CPSS.Service.ViewService.ViewModels.SubCompany.Respond;
using CPSS.Web.Controllers.Filters;

namespace CPSS.Web.Controllers
{
    [HaveToLogin]
    public class BasicController : WebBaseController
    {
        #region private readonly fields

        private readonly ISubCompanyViewService mSubCompanyViewService;

        #endregion

        #region 构造函数
        public BasicController(ISubCompanyViewService subCompanyViewService)
        {
            this.mSubCompanyViewService = subCompanyViewService;
        }

        #endregion

        #region 公司资料

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicCom)]
        public ActionResult CompanyList()
        {
            return View();
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicCom_TB_Add)]
        public ActionResult AddCompany()
        {
            return View();
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicCom_TB_Edit)]
        public ActionResult EditCompany()
        {
            var comid = this.WorkContext.GetQueryInt("comid");
            var model = new object();
            return View(model);
        }

        #region ajax操作方法

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicCom)]
        [HttpPost]
        public JsonResult GetCompanyList(RequestWebViewData<RequestQuerySubCompanyViewModel> request)
        {
            var respond = this.mSubCompanyViewService.GetQueryCompanyList(request);
            return Json(respond);
        }
        

        #endregion

        #endregion
    }
}