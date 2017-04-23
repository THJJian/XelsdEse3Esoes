using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CPSS.Common.Core;
using CPSS.Common.Core.Helper.Cached;
using CPSS.Common.Core.Type.EnumType;
using CPSS.Data.DataAccess.Interfaces;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters.Employee;
using CPSS.Data.DataAccess.Interfaces.MongoDB;
using CPSS.Service.ViewService.Interfaces.Basic;
using CPSS.Service.ViewService.ViewModels.Department.Respond;
using CPSS.Service.ViewService.ViewModels.Employee.Request;
using CPSS.Service.ViewService.ViewModels.Employee.Respond;

namespace CPSS.Service.ViewService.Basic
{
    public class EmployeeViewService: BaseViewService, IEmployeeViewService
    {
        private const string THISSERVICE_PRE_CACHE_KEY_MANAGE = "CPSS.Service.ViewService.Basic.EmployeeViewService";
        private const string PRE_CACHE_KEY = "CPSS.Service.ViewService.Basic.EmployeeViewService.{0}";
        private readonly IDbConnection mDbConnection;
        private readonly IemployeeDataAccess mEmployeeDataAccess;


        public EmployeeViewService(IMongoDbDataAccess mongoDbDataAccess, IDbConnection dbConnection, IemployeeDataAccess employeeDataAccess) : base(mongoDbDataAccess)
        {
            this.mDbConnection = dbConnection;
            this.mEmployeeDataAccess = employeeDataAccess;
        }

        public RespondWebViewData<List<RespondQueryEmployeeViewModel>> GetQueryDepartmentList(RequestWebViewData<RequestQueryEmployeeViewModel> request)
        {
            if (request.data == null) request.data = new RequestQueryEmployeeViewModel();
            return MemcacheHelper.Get(new RequestMemcacheParameter<RespondWebViewData<List<RespondQueryEmployeeViewModel>>>
            {
                CacheKey = string.Format(PRE_CACHE_KEY, "GetQueryDepartmentList"),

                #region =================
                CallBackFunc = () =>
                {
                    var parameter = new QueryEmployeeListParameter()
                    {
                        Comment = request.data.Comment,
                        Deleted = request.data.Deleted,
                        DepIds = request.data.DepIds,
                        SerialNumber = request.data.SerialNumber,
                        Mobile = request.data.Mobile,
                        Name = request.data.Name,
                        PageIndex = request.page,
                        PageSize = request.rows,
                        Status = request.data.Status,
                        ParentId = request.data.ParentId,
                        Spelling = request.data.Spelling,
                    };
                    var pageDataList = this.mEmployeeDataAccess.GetQueryEmployeeList(parameter);
                    var respond = new RespondWebViewData<List<RespondQueryEmployeeViewModel>>
                    {
                        total = pageDataList.DataCount,
                        rows = pageDataList.Datas.Select(item => new RespondQueryEmployeeViewModel
                        {
                            Address = item.address,
                            ChildNumber = item.childnumber,
                            ClassId = item.classid,
                            Comment = item.comment,
                            Deleted = item.deleted.HasValue ? item.deleted.Value : (short)CommonDeleted.NotDeleted,
                            DepName = item.depname,
                            EmpId = item.empid,
                            LowestDiscount = item.lowestdiscount.HasValue ? item.lowestdiscount.Value : (short)100,
                            Mobile = item.mobile,
                            Name = item.name,
                            ParentId = item.parentid,
                            PreInAdvanceTotal = item.preinadvancetotal.HasValue ? item.preinadvancetotal.Value : 0,
                            PrePayFeeTotal = item.prepayfeetotal.HasValue ? item.prepayfeetotal.Value : 0,
                            SerialNumber = item.serialnumber,
                            Sort = item.sort.HasValue ? item.sort.Value : 0,
                            Spelling = item.pinyin,
                            Status = item.status.HasValue ? item.status.Value : 1
                        }).ToList()
                    };
                    return respond;
                },
                #endregion

                ExpiresAt = DateTime.Now.AddMinutes(30),
                ManageCacheKeyForKey = THISSERVICE_PRE_CACHE_KEY_MANAGE,
                ParamsKeys = new object[]
                {
                    request.data.Status,
                    request.data.Comment,
                    string.Join("/", request.data.DepIds),
                    request.data.Mobile,
                    request.data.Deleted,
                    request.data.Name,
                    request.data.ParentId,
                    request.data.SerialNumber,
                    request.data.Spelling,
                    request.page,
                    request.rows
                }
            });
        }

        public RespondWebViewData<RespondAddEmployeeViewModel> AddDepartment(RequestWebViewData<RequestAddEmployeeViewModel> request)
        {
            throw new System.NotImplementedException();
        }

        public RespondWebViewData<RespondQueryEmployeeViewModel> GetDepartmentByDepId(RequestWebViewData<RequestGetEmployeeByIdViewModel> request)
        {
            throw new System.NotImplementedException();
        }

        public RespondWebViewData<RequestEditEmployeeViewModel> EditDepartment(RequestWebViewData<RequestEditEmployeeViewModel> request)
        {
            throw new System.NotImplementedException();
        }

        public RespondWebViewData<RespondDeleteEmployeeViewModel> DeleteDepartment(RequestWebViewData<RequestDeleteEmployeeViewModel> request)
        {
            throw new System.NotImplementedException();
        }

        public RespondWebViewData<RespondDeleteEmployeeViewModel> ReDeleteDepartment(RequestWebViewData<RequestDeleteEmployeeViewModel> request)
        {
            throw new System.NotImplementedException();
        }
    }
}