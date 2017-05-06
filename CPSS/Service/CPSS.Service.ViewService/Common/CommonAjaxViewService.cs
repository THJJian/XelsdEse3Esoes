using System;
using System.Collections.Generic;
using System.Linq;
using CPSS.Common.Core;
using CPSS.Common.Core.Helper.Cached;
using CPSS.Common.Core.Type.ConstDefined;
using CPSS.Data.DataAccess.Interfaces;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters.Department;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters.Employee;
using CPSS.Data.DataAccess.Interfaces.MongoDB;
using CPSS.Service.ViewService.Interfaces.Common;
using CPSS.Service.ViewService.ViewModels.Common.Request;
using CPSS.Service.ViewService.ViewModels.Common.Respond;

namespace CPSS.Service.ViewService.Common
{
    public class CommonAjaxViewService : BaseViewService, ICommonAjaxViewService
    {
        private const string PRE_CACHE_KEY = "CPSS.Service.ViewService.Basic.DepartmentViewService.{0}";
        private readonly IdepartmentDataAccess mDepartmentDataAccess;
        private readonly IemployeeDataAccess mEmployeeDataAccess;

        public CommonAjaxViewService(IMongoDbDataAccess mongoDbDataAccess, IdepartmentDataAccess departmentDataAccess, IemployeeDataAccess employeeDataAccess) : base(mongoDbDataAccess)
        {
            this.mDepartmentDataAccess = departmentDataAccess;
            this.mEmployeeDataAccess = employeeDataAccess;
        }

        public List<RespondGetAllDepartmentViewModel> GetAllDepartment(RequestWebViewData<RequestGetAllEnityDataViewModel> request)
        {
            return MemcacheHelper.Get(new RequestMemcacheParameter<List<RespondGetAllDepartmentViewModel>>
            {
                CacheKey = string.Format(PRE_CACHE_KEY, "GetAllDepartment"),

                #region =====================
                CallBackFunc = () =>
                {
                    var parameter = new QueryDepartmentListParameter
                    {
                        SerialNumber = request.data.Keywords
                    };
                    var dataList = this.mDepartmentDataAccess.GetAllDepartment(parameter);
                    var respond = dataList.Select(item => new RespondGetAllDepartmentViewModel
                    {
                        Comment = item.comment,
                        DepId = item.depid,
                        Name = item.name,
                        SerialNumber = item.serialnumber,
                        Spelling = item.pinyin
                    }).ToList();
                    return respond;
                },
                #endregion
                
                ManageCacheKeyForKey = ServiceMemcachedKeyManageConst.CommonAjax,
                ExpiresAt = DateTime.Now.AddMinutes(30),
                ParamsKeys = new object[]
                {
                    request.data.Keywords,
                    this.mSigninUser.UserID
                }
            });
        }


        public List<RespondGetAllEmployeeViewModel> GetAllEmployee(RequestWebViewData<RequestGetAllEnityDataViewModel> request)
        {
            return MemcacheHelper.Get(new RequestMemcacheParameter<List<RespondGetAllEmployeeViewModel>>
            {
                CacheKey = string.Format(PRE_CACHE_KEY, "GetAllEmployee"),

                #region =====================
                CallBackFunc = () =>
                {
                    var parameter = new QueryEmployeeListParameter
                    {
                        SerialNumber = request.data.Keywords
                    };
                    var dataList = this.mEmployeeDataAccess.GetAllEmployee(parameter);
                    var respond = dataList.Select(item => new RespondGetAllEmployeeViewModel
                    {
                        EmpId = item.empid,
                        SerialNumber = item.serialnumber,
                        Name = item.name,
                        Comment = item.comment,
                        Spelling = item.pinyin
                    }).ToList();
                    return respond;
                },
                #endregion

                ManageCacheKeyForKey = ServiceMemcachedKeyManageConst.CommonAjax,
                ExpiresAt = DateTime.Now.AddMinutes(30),
                ParamsKeys = new object[]
                {
                    request.data.Keywords,
                    this.mSigninUser.UserID
                }
            });
        }
    }
}