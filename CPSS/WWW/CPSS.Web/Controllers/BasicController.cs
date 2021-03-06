﻿using System.Web.Mvc;
using CPSS.Common.Core;
using CPSS.Common.Core.Mvc;
using CPSS.Common.Core.Mvc.Filters;
using CPSS.Common.Core.Type.ConstDefined;
using CPSS.Service.ViewService.Interfaces.Basic;
using CPSS.Service.ViewService.ViewModels.Client.Request;
using CPSS.Service.ViewService.ViewModels.Department.Request;
using CPSS.Service.ViewService.ViewModels.SubCompany.Request;
using CPSS.Web.Controllers.Filters;
using CPSS.Service.ViewService.ViewModels.Employee.Request;
using CPSS.Service.ViewService.ViewModels.Product.Request;
using CPSS.Service.ViewService.ViewModels.Storage.Request;
using CPSS.Service.ViewService.ViewModels.Unit.Request;

namespace CPSS.Web.Controllers
{
    /// <summary>
    /// 基础资料
    /// </summary>
    [HaveToLogin]
    public class BasicController : WebBaseController
    {
        #region private readonly fields

        private readonly ISubCompanyViewService mSubCompanyViewService;
        private readonly IDepartmentViewService mDepartmentViewService;
        private readonly IEmployeeViewService mEmployeeViewService;
        private readonly IClientViewService mClientViewService;
        private readonly IStorageViewService mStorageViewService;
        private readonly IUnitViewService mUnitViewService;
        private readonly IProductViewService mProductViewService;

        #endregion

        #region 构造函数
        public BasicController(ISubCompanyViewService subCompanyViewService, IDepartmentViewService departmentViewService, IEmployeeViewService employeeViewService, IClientViewService clientViewService,
            IStorageViewService storageViewService, IUnitViewService unitViewService, IProductViewService productViewService)
        {
            this.mSubCompanyViewService = subCompanyViewService;
            this.mDepartmentViewService = departmentViewService;
            this.mEmployeeViewService = employeeViewService;
            this.mClientViewService = clientViewService;
            this.mStorageViewService = storageViewService;
            this.mUnitViewService = unitViewService;
            this.mProductViewService = productViewService;
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

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicDep_TB_Add)]
        [HttpPost]
        public JsonResult AddDepartment(RequestWebViewData<RequestAddDepartmentViewModel> request)
        {
            var respond = this.mDepartmentViewService.AddDepartment(request);
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicDep_TB_Edit)]
        [HttpPost]
        public JsonResult EditDepartment(RequestWebViewData<RequestEditDepartmentViewModel> request)
        {
            var respond = this.mDepartmentViewService.EditDepartment(request);
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicDep_TB_Delete)]
        [HttpPost]
        public JsonResult DeleteDepartment(RequestWebViewData<RequestDeleteDepartmentViewModel> request)
        {
            var respond = this.mDepartmentViewService.DeleteDepartment(request);
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicDep_TB_Resume)]
        [HttpPost]
        public JsonResult ReDeleteDepartment(RequestWebViewData<RequestDeleteDepartmentViewModel> request)
        {
            var respond = this.mDepartmentViewService.ReDeleteDepartment(request);
            return Json(respond);
        }

        #endregion

        #endregion

        #region 职员资料

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicEmp)]
        public ActionResult EmployeeList()
        {
            return View("~/views/basic/employee/employeelist.cshtml");
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicEmp_TB_Add)]
        public ActionResult AddEmployee()
        {
            return View("~/views/basic/employee/addemployee.cshtml");
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicEmp_TB_Edit)]
        public ActionResult EditEmployee()
        {
            var empId = this.WorkContext.GetQueryInt("empid");
            var request = new RequestWebViewData<RequestGetEmployeeByIdViewModel>
            {
                data = new RequestGetEmployeeByIdViewModel
                {
                    EmpId = empId
                }
            };
            var model = this.mEmployeeViewService.GetEmployeeByEmpId(request);
            return View("~/views/basic/employee/editemployee.cshtml", model);
        }

        #region Ajax操作方法

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicEmp)]
        [HttpPost]
        public JsonResult GetEmployeeList(RequestWebViewData<RequestQueryEmployeeViewModel> request)
        {
            if (string.IsNullOrEmpty(request.data.ParentId)) request.data.ParentId = "000001";
            var respond = this.mEmployeeViewService.GetQueryEmployeeList(request);
            respond.parentId = request.data.ParentId;
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicEmp_TB_Add)]
        [HttpPost]
        public JsonResult AddEmployee(RequestWebViewData<RequestAddEmployeeViewModel> request)
        {
            var respond = this.mEmployeeViewService.AddEmployee(request);
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicEmp_TB_Edit)]
        [HttpPost]
        public JsonResult EditEmployee(RequestWebViewData<RequestEditEmployeeViewModel> request)
        {
            var respond = this.mEmployeeViewService.EditEmployee(request);
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicEmp_TB_Delete)]
        [HttpPost]
        public JsonResult DeleteEmployee(RequestWebViewData<RequestDeleteEmployeeViewModel> request)
        {
            var respond = this.mEmployeeViewService.DeleteEmployee(request);
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicEmp_TB_Resume)]
        [HttpPost]
        public JsonResult ReDeleteEmployee(RequestWebViewData<RequestDeleteEmployeeViewModel> request)
        {
            var respond = this.mEmployeeViewService.ReDeleteEmployee(request);
            return Json(respond);
        }

        #endregion

        #endregion

        #region 往来客户资料

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicClient)]
        public ActionResult ClientList()
        {
            return View("~/views/basic/client/clientlist.cshtml");
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicClient_TB_Add)]
        public ActionResult AddClient()
        {
            return View("~/views/basic/client/addclient.cshtml");
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicClient_TB_Edit)]
        public ActionResult EditClient()
        {
            var clientId = this.WorkContext.GetQueryInt("cid");
            var request = new RequestWebViewData<RequestGetClientByIdViewModel>
            {
                data = new RequestGetClientByIdViewModel
                {
                    ClientId = clientId
                }
            };
            var model = this.mClientViewService.GetClientByClientId(request);
            return View("~/views/basic/client/editclient.cshtml", model);
        }

        #region Ajax操作方法

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicClient)]
        [HttpPost]
        public JsonResult GetClientList(RequestWebViewData<RequestQueryClientViewModel> request)
        {
            if (string.IsNullOrEmpty(request.data.ParentId)) request.data.ParentId = "000001";
            var respond = this.mClientViewService.GetQueryClientList(request);
            respond.parentId = request.data.ParentId;
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicClient_TB_Add)]
        [HttpPost]
        public JsonResult AddClient(RequestWebViewData<RequestAddClientViewModel> request)
        {
            var respond = this.mClientViewService.AddClient(request);
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicClient_TB_Edit)]
        [HttpPost]
        public JsonResult EditClient(RequestWebViewData<RequestEditClientViewModel> request)
        {
            var respond = this.mClientViewService.EditClient(request);
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicClient_TB_Delete)]
        [HttpPost]
        public JsonResult DeleteClient(RequestWebViewData<RequestDeleteClientViewModel> request)
        {
            var respond = this.mClientViewService.DeleteClient(request);
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicClient_TB_Resume)]
        [HttpPost]
        public JsonResult ReDeleteClient(RequestWebViewData<RequestDeleteClientViewModel> request)
        {
            var respond = this.mClientViewService.ReDeleteClient(request);
            return Json(respond);
        }

        #endregion

        #endregion

        #region 仓库资料

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicStorage)]
        public ActionResult StorageList()
        {
            return View("~/views/basic/storage/storagelist.cshtml");
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicStorage_TB_Add)]
        public ActionResult AddStorage()
        {
            return View("~/views/basic/storage/addstorage.cshtml");
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicStorage_TB_Edit)]
        public ActionResult EditStorage()
        {
            var storageId = this.WorkContext.GetQueryInt("sid");
            var request = new RequestWebViewData<RequestGetStorageByIdViewModel>
            {
                data = new RequestGetStorageByIdViewModel
                {
                    StorageId = storageId
                }
            };
            var model = this.mStorageViewService.GetStorageByStorageId(request);
            return View("~/views/basic/storage/editstorage.cshtml", model);
        }

        #region Ajax操作方法

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicStorage)]
        [HttpPost]
        public JsonResult GetStorageList(RequestWebViewData<RequestQueryStorageViewModel> request)
        {
            if (string.IsNullOrEmpty(request.data.ParentId)) request.data.ParentId = "000001";
            var respond = this.mStorageViewService.GetQueryStorageList(request);
            respond.parentId = request.data.ParentId;
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicStorage_TB_Add)]
        [HttpPost]
        public JsonResult AddStorage(RequestWebViewData<RequestAddStorageViewModel> request)
        {
            var respond = this.mStorageViewService.AddStorage(request);
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicStorage_TB_Edit)]
        [HttpPost]
        public JsonResult EditStorage(RequestWebViewData<RequestEditStorageViewModel> request)
        {
            var respond = this.mStorageViewService.EditStorage(request);
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicStorage_TB_Delete)]
        [HttpPost]
        public JsonResult DeleteStorage(RequestWebViewData<RequestDeleteStorageViewModel> request)
        {
            var respond = this.mStorageViewService.DeleteStorage(request);
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicStorage_TB_Resume)]
        [HttpPost]
        public JsonResult ReDeleteStorage(RequestWebViewData<RequestDeleteStorageViewModel> request)
        {
            var respond = this.mStorageViewService.ReDeleteStorage(request);
            return Json(respond);
        }

        #endregion

        #endregion

        #region 计量单位

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicUnit)]
        public ActionResult UnitList()
        {
            return View("~/views/basic/unit/unitlist.cshtml");
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicUnit_TB_Add)]
        public ActionResult AddUnit()
        {
            return View("~/views/basic/unit/addunit.cshtml");
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicUnit_TB_Edit)]
        public ActionResult EditUnit()
        {
            var unitId = this.WorkContext.GetQueryInt("unitid");
            var request = new RequestWebViewData<RequestGetUnitByIdViewModel>
            {
                data = new RequestGetUnitByIdViewModel
                {
                    UnitId = unitId
                }
            };
            var model = this.mUnitViewService.GetUnitByUnitId(request);
            return View("~/views/basic/unit/editunit.cshtml", model);
        }

        #region Ajax操作方法

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicUnit)]
        [HttpPost]
        public JsonResult GetUnitList(RequestWebViewData<RequestQueryUnitViewModel> request)
        {
            var respond = this.mUnitViewService.GetQueryUnitList(request);
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicUnit_TB_Add)]
        [HttpPost]
        public JsonResult AddUnit(RequestWebViewData<RequestAddUnitViewModel> request)
        {
            var respond = this.mUnitViewService.AddUnit(request);
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicUnit_TB_Edit)]
        [HttpPost]
        public JsonResult EditUnit(RequestWebViewData<RequestEditUnitViewModel> request)
        {
            var respond = this.mUnitViewService.EditUnit(request);
            return Json(respond);
        }
        
        [OperateRight(MenuID = MenuValueConstDefined.rtBasicUnit_TB_Delete)]
        [HttpPost]
        public JsonResult DeleteUnit(RequestWebViewData<RequestDeleteUnitViewModel> request)
        {
            var respond = this.mUnitViewService.DeleteUnit(request);
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicUnit_TB_Resume)]
        [HttpPost]
        public JsonResult ReDeleteUnit(RequestWebViewData<RequestDeleteUnitViewModel> request)
        {
            var respond = this.mUnitViewService.ReDeleteUnit(request);
            return Json(respond);
        }

        #endregion

        #endregion

        #region 商品资料

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicProduct)]
        public ActionResult ProductList()
        {
            return View("~/views/basic/product/productlist.cshtml");
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicProduct_TB_Add)]
        public ActionResult AddProduct()
        {
            return View("~/views/basic/product/addproduct.cshtml");
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicProduct_TB_Edit)]
        public ActionResult EditProduct()
        {
            var proId = this.WorkContext.GetQueryInt("proid");
            var request = new RequestWebViewData<RequestGetProductByIdViewModel>
            {
                data = new RequestGetProductByIdViewModel
                {
                    ProId = proId
                }
            };
            var model = this.mProductViewService.GetProductByProId(request);
            return View("~/views/basic/product/editproduct.cshtml", model);
        }

        #region Ajax操作方法

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicProduct)]
        [HttpPost]
        public JsonResult GetProductList(RequestWebViewData<RequestQueryProductViewModel> request)
        {
            if (string.IsNullOrEmpty(request.data.ParentId)) request.data.ParentId = "000001";
            var respond = this.mProductViewService.GetQueryProductList(request);
            respond.parentId = request.data.ParentId;
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicProduct_TB_Add)]
        [HttpPost]
        public JsonResult AddProduct(RequestWebViewData<RequestAddProductViewModel> request)
        {
            var respond = this.mProductViewService.AddProduct(request);
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicProduct_TB_Edit)]
        [HttpPost]
        public JsonResult EditProduct(RequestWebViewData<RequestEditProductViewModel> request)
        {
            var respond = this.mProductViewService.EditProduct(request);
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicProduct_TB_Delete)]
        [HttpPost]
        public JsonResult DeleteProduct(RequestWebViewData<RequestDeleteProductViewModel> request)
        {
            var respond = this.mProductViewService.DeleteProduct(request);
            return Json(respond);
        }

        [OperateRight(MenuID = MenuValueConstDefined.rtBasicProduct_TB_Resume)]
        [HttpPost]
        public JsonResult ReDeleteProduct(RequestWebViewData<RequestDeleteProductViewModel> request)
        {
            var respond = this.mProductViewService.ReDeleteProduct(request);
            return Json(respond);
        }

        #endregion

        #endregion

    }
}