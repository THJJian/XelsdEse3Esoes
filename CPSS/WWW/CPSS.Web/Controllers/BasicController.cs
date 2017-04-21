﻿using System.Web.Mvc;
using CPSS.Common.Core;
using CPSS.Common.Core.Mvc;
using CPSS.Common.Core.Mvc.Filters;
using CPSS.Common.Core.Type.ConstDefined;
using CPSS.Service.ViewService.Interfaces.Basic;
using CPSS.Service.ViewService.ViewModels.Department.Request;
using CPSS.Service.ViewService.ViewModels.SubCompany.Request;
using CPSS.Web.Controllers.Filters;

namespace CPSS.Web.Controllers
{
    [HaveToLogin]
    public class BasicController : WebBaseController
    {
        #region private readonly fields

        private readonly ISubCompanyViewService mSubCompanyViewService;
        private readonly IDepartmentViewService mDepartmentViewService;

        #endregion

        #region 构造函数
        public BasicController(ISubCompanyViewService subCompanyViewService, IDepartmentViewService departmentViewService)
        {
            this.mSubCompanyViewService = subCompanyViewService;
            this.mDepartmentViewService = departmentViewService;
        }

        #endregion

        #region 公司资料

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicCom)]
        public ActionResult CompanyList()
        {
            return View("~/Views/Basic/SubCompany/CompanyList.cshtml");
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicCom_TB_Add)]
        public ActionResult AddCompany()
        {
            return View("~/views/basic/subcompany/addcompany.cshtml");
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicCom_TB_Edit)]
        public ActionResult EditCompany()
        {
            var comid = this.WorkContext.GetQueryInt("comid");
            var request = new RequestWebViewData<RequestGetSubCompanyByIdViewModel>
            {
                data = new RequestGetSubCompanyByIdViewModel
                {
                    ComId = comid
                }
            };
            var model = this.mSubCompanyViewService.GetSubCompanyByComId(request);
            return View("~/views/basic/subcompany/editcompany.cshtml", model);
        }

        #region ajax操作方法

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicCom)]
        [HttpPost]
        public JsonResult GetCompanyList(RequestWebViewData<RequestQuerySubCompanyViewModel> request)
        {
            if (string.IsNullOrEmpty(request.data.ParentId)) request.data.ParentId = "000001";
            var respond = this.mSubCompanyViewService.GetQueryCompanyList(request);
            respond.parentId = request.data.ParentId;
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicCom_TB_Add)]
        [HttpPost]
        public JsonResult AddCompany(RequestWebViewData<RequestAddSubCompanyViewModel> request)
        {
            var respond = this.mSubCompanyViewService.AddSubCompany(request);
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicCom_TB_Edit)]
        [HttpPost]
        public JsonResult EditCompany(RequestWebViewData<RequestEditSubCompanyViewModel> request)
        {
            var respond = this.mSubCompanyViewService.EditSubCompany(request);
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicCom_TB_Delete)]
        [HttpPost]
        public JsonResult DeleteCompany(RequestWebViewData<RequestDeleteSubCompanyViewModel> request)
        {
            var respond = this.mSubCompanyViewService.DeleteSubCompany(request);
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicCom_TB_Resume)]
        [HttpPost]
        public JsonResult ReDeleteCompany(RequestWebViewData<RequestDeleteSubCompanyViewModel> request)
        {
            var respond = this.mSubCompanyViewService.ReDeleteSubCompany(request);
            return Json(respond);
        }
        #endregion

        #endregion

        #region 部门资料

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicDep)]
        public ActionResult DepartmentList()
        {
            return View("~/views/basic/department/departmentlist.cshtml");
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicDep_TB_Add)]
        public ActionResult AddDepartment()
        {
            return View("~/views/basic/department/adddepartment.cshtml");
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicDep_TB_Edit)]
        public ActionResult EditDepartment()
        {
            var depid = this.WorkContext.GetQueryInt("depid");
            var request = new RequestWebViewData<RequestGetDepartmentByIdViewModel>
            {
                data = new RequestGetDepartmentByIdViewModel
                {
                    DepId = depid
                }
            };
            var model = this.mDepartmentViewService.GetDepartmentByDepId(request);
            return View("~/views/basic/department/editdepartment.cshtml", model);
        }

        #region Ajax操作方法

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicDep)]
        [HttpPost]
        public JsonResult GetDepartmentList(RequestWebViewData<RequestQueryDepartmentViewModel> request)
        {
            if (string.IsNullOrEmpty(request.data.ParentId)) request.data.ParentId = "000001";
            var respond = this.mDepartmentViewService.GetQueryDepartmentList(request);
            respond.parentId = request.data.ParentId;
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicCom_TB_Add)]
        [HttpPost]
        public JsonResult AddDepartment(RequestWebViewData<RequestAddDepartmentViewModel> request)
        {
            var respond = this.mDepartmentViewService.AddDepartment(request);
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicCom_TB_Edit)]
        [HttpPost]
        public JsonResult EditDepartment(RequestWebViewData<RequestEditDepartmentViewModel> request)
        {
            var respond = this.mDepartmentViewService.EditDepartment(request);
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicCom_TB_Delete)]
        [HttpPost]
        public JsonResult DeleteDepartment(RequestWebViewData<RequestDeleteDepartmentViewModel> request)
        {
            var respond = this.mDepartmentViewService.DeleteDepartment(request);
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicCom_TB_Resume)]
        [HttpPost]
        public JsonResult ReDeleteDepartment(RequestWebViewData<RequestDeleteDepartmentViewModel> request)
        {
            var respond = this.mDepartmentViewService.ReDeleteDepartment(request);
            return Json(respond);
        }

        #endregion

        #endregion
    }
}