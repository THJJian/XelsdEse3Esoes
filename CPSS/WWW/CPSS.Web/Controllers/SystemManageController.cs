using System.Web.Mvc;
using CPSS.Common.Core;
using CPSS.Common.Core.Mvc;
using CPSS.Common.Core.Mvc.Filters;
using CPSS.Common.Core.Type.ConstDefined;
using CPSS.Service.ViewService.Interfaces.SystemManage;
using CPSS.Service.ViewService.Interfaces.SystemManage.UserManage;
using CPSS.Service.ViewService.ViewModels.SystemManage.UserManage.Request;
using CPSS.Service.ViewService.ViewModels.SystemManage.Request;
using CPSS.Web.Controllers.Filters;

namespace CPSS.Web.Controllers
{
    /// <summary>
    /// 系统管理
    /// </summary>
    [HaveToLogin]
    public class SystemManageController : WebBaseController
    {
        #region Private Field

        private readonly ISystemParameterConfigViewService mSystemParameterConfigViewService;
        private readonly IUserManageViewService mUserManageViewService;

        #endregion

        #region 构造函数

        public SystemManageController(ISystemParameterConfigViewService systemParameterConfigViewService, IUserManageViewService userManageViewService)
        {
            this.mSystemParameterConfigViewService = systemParameterConfigViewService;
            this.mUserManageViewService = userManageViewService;
        }
        

        #endregion

        #region 系统参数

        [OperateRight(MenuID = MenuValueConstDefined.rtSystemParameter)]
        public ActionResult SysConfig()
        {
            var viewModels = this.mSystemParameterConfigViewService.GetSystemParameterConfigViewModels();
            return View("~/views/systemmanage/parameterconfig/sysconfig.cshtml", viewModels);
        }

        [HttpPost]
        [OperateRight(MenuID = MenuValueConstDefined.rtSystemParameter)]
        public JsonResult SaveSystemParameterConfig(RequestWebViewData<RequestSystemParameterConfigListViewModel> request)
        {
            var respond = this.mSystemParameterConfigViewService.SaveSystemParameterConfig(request);
            return Json(respond);
        }
        
        #endregion

        #region 用户管理

        [OperateRight(MenuID = MenuValueConstDefined.rtUserManage)]
        public ActionResult UserManageList()
        {
            return View("~/views/systemmanage/users/usermanagelist.cshtml");
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtUserManage_TB_Add)]
        public ActionResult AddUser()
        {
            return View("~/views/systemmanage/users/adduser.cshtml");
        }

        public ActionResult EditUser()
        {
            var userId = this.WorkContext.GetQueryInt("uid");
            var request = new RequestWebViewData<RequestQueryUserViewModel>
            {
                data = new RequestQueryUserViewModel
                {
                    UserId = userId
                }
            };
            var respond = this.mUserManageViewService.GetUserDataByUserId(request);
            return View("~/views/systemmanage/users/edituser.cshtml", respond);
        }

        #region Ajax操作Action

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperateRight(MenuID = MenuValueConstDefined.rtUserManage)]
        [HttpPost]
        public JsonResult GetUserList(RequestWebViewData<RequestQueryUserViewModel> request)
        {
            var respond = this.mUserManageViewService.GetUserList(request);
            return Json(respond);
        }

        #endregion

        #endregion

    }
}