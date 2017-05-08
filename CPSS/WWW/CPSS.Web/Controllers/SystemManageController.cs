﻿using System.Web.Mvc;
using CPSS.Common.Core;
using CPSS.Common.Core.Mvc;
using CPSS.Common.Core.Mvc.Filters;
using CPSS.Common.Core.Type.ConstDefined;
using CPSS.Service.ViewService.Interfaces.SystemParameterConfig;
using CPSS.Service.ViewService.ViewModels.SystemParameterConfig.Request;
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

        #endregion

        #region 构造函数

        public SystemManageController(ISystemParameterConfigViewService _systemParameterConfigViewService)
        {
            this.mSystemParameterConfigViewService = _systemParameterConfigViewService;
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

        }

        #endregion

        #endregion

    }
}