using System;
using System.Collections.Generic;
using System.Linq;
using CPSS.Common.Core;
using CPSS.Common.Core.Helper.Cached;
using CPSS.Data.DataAccess.Interfaces;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters.Department;
using CPSS.Data.DataAccess.Interfaces.MongoDB;
using CPSS.Service.ViewService.Interfaces.Common;
using CPSS.Service.ViewService.ViewModels.Common.Request;
using CPSS.Service.ViewService.ViewModels.Common.Respond;

namespace CPSS.Service.ViewService.Common
{
    public class CommonAjaxViewService : BaseViewService, ICommonAjaxViewService
    {
        //private const string THISSERVICE_PRE_CACHE_KEY_MANAGE = "CPSS.Service.ViewService.Basic.DepartmentViewService"; TODO 要用时在取消注释
        private const string PRE_CACHE_KEY = "CPSS.Service.ViewService.Basic.DepartmentViewService.{0}";
        private readonly IdepartmentDataAccess mDepartmentDataAccess;

        public CommonAjaxViewService(IMongoDbDataAccess mongoDbDataAccess, IdepartmentDataAccess departmentDataAccess) : base(mongoDbDataAccess)
        {
            this.mDepartmentDataAccess = departmentDataAccess;
        }

        public List<RespondGetAllDepartmentViewModel> GetAllDepartment(RequestWebViewData<RequestGetAllDepartmentViewModel> request)
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