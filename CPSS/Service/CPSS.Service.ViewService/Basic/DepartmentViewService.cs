using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CPSS.Common.Core;
using CPSS.Common.Core.Authenticate;
using CPSS.Common.Core.DataAccess.DataAccess;
using CPSS.Common.Core.Exception;
using CPSS.Common.Core.Helper.Cached;
using CPSS.Common.Core.Helper.Generated;
using CPSS.Data.DataAccess.Interfaces;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters;
using CPSS.Data.DataAccess.Interfaces.MongoDB;
using CPSS.Data.DataAcess.DataModels;
using CPSS.Service.ViewService.Interfaces.Basic;
using CPSS.Service.ViewService.ViewModels.Department.Request;
using CPSS.Service.ViewService.ViewModels.Department.Respond;
using CPSS.Service.ViewService.ViewModels.MongoDb.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CPSS.Service.ViewService.Basic
{
    public class DepartmentViewService : IDepartmentViewService
    {
        private const string THISSERVICE_PRE_CACHE_KEY_MANAGE = "CPSS.Service.ViewService.Basic.DepartmentViewService";
        private const string PRE_CACHE_KEY = "CPSS.Service.ViewService.Basic.DepartmentViewService.{0}";
        private readonly IDbConnection mDbConnection;
        private readonly IdepartmentDataAccess mDepartmentDataAccess;
        private readonly IMongoDbDataAccess mMongoDbDataAccess;
        private readonly SigninUser mSigninUser;

        public DepartmentViewService(IDbConnection dbConnection, IdepartmentDataAccess departmentDataAccess, IMongoDbDataAccess mongoDbDataAccess)
        {
            mDbConnection = dbConnection;
            this.mDepartmentDataAccess = departmentDataAccess;
            mMongoDbDataAccess = mongoDbDataAccess;
            mSigninUser = CPSSAuthenticate.GetCurrentUser();
        }


        public RespondWebViewData<List<RespondQueryDepartmentViewModel>> GetQueryDepartmentList(RequestWebViewData<RequestQueryDepartmentViewModel> request)
        {
            if (request.data == null) request.data = new RequestQueryDepartmentViewModel();
            return MemcacheHelper.Get(new RequestMemcacheParameter<RespondWebViewData<List<RespondQueryDepartmentViewModel>>>
            {
                CacheKey = string.Format(PRE_CACHE_KEY, "GetQueryDepartmentList"),

                #region =================
                CallBackFunc = () =>
                {
                    var parameter = new QueryDepartmentListParameter
                    {
                        Deleted = request.data.Deleted,
                        SerialNumber = request.data.SerialNumber,
                        Name = request.data.Name,
                        PageIndex = request.page,
                        PageSize = request.rows,
                        Status = request.data.Status
                    };
                    var pageDataList = this.mDepartmentDataAccess.GetQueryDepartmentList(parameter);
                    var respond = new RespondWebViewData<List<RespondQueryDepartmentViewModel>>
                    {
                        total = pageDataList.DataCount,
                        rows = pageDataList.Datas.Select(item=> new RespondQueryDepartmentViewModel
                        {
                            ChildNumber = item.childnumber,
                            ClassId = item.classid,
                            ChildCount = item.childcount,
                            Comment = item.comment,
                            Deleted = item.deleted.HasValue ? item.deleted.Value : (short)1,
                            DepId = item.depid,
                            Name = item.name,
                            ParentId = item.parentid,
                            SerialNumber = item.serialnumber,
                            Sort = item.sort,
                            Spelling = item.pinyin,
                            Status = item.status
                        }).ToList()
                    };
                    return respond;
                },
                #endregion
                
                ExpiresAt = DateTime.Now.AddMinutes(30),
                ManageCacheKeyForKey = THISSERVICE_PRE_CACHE_KEY_MANAGE,
                ParamsKeys = new object[]
                {
                    request.data.Deleted,
                    request.data.SerialNumber,
                    request.data.Name,
                    request.page,
                    request.rows,
                    request.data.Status
                }
            });
        }

        public RespondWebViewData<RespondAddDepartmentViewModel> AddDepartment(RequestWebViewData<RequestAddDepartmentViewModel> request)
        {
            var respond = new RespondWebViewData<RespondAddDepartmentViewModel>(WebViewErrorCode.Success);
            var rData = request.data;
            this.mDbConnection.ExecuteTransaction(tran =>
            {
                var parameter = new QueryDepartmentListParameter()
                {
                    ParentId = rData.ParentId
                };
                var classId = string.Concat(rData.ParentId, "000001");
                var depList = this.mDepartmentDataAccess.GetDepartmentListByParentID(parameter);
                if (depList.Count > 0)
                    classId = BuildNewClassIdByLastClassId.GeneratedNewClassIdByLastClassId(depList[0].classid);

                var data = new departmentDataModel
                {
                    childnumber = 0,
                    classid = classId,
                    comment = rData.Comment,
                    name = rData.Name,
                    parentid = rData.ParentId,
                    pinyin = rData.Spelling,
                    serialnumber = rData.SerialNumber,
                    sort = rData.Sort,
                    status = 1,
                    deleted = 1
                };
                var addResult = this.mDepartmentDataAccess.Add(data, tran);
                if (addResult > 0) this.mDepartmentDataAccess.UpdateChildNumberByClassId(tran, parameter);
                MemcacheHelper.RemoveBy(THISSERVICE_PRE_CACHE_KEY_MANAGE);
                var mongo_db_request = new RequestMongoDbViewModel
                {
                    LogName = "新增部门资料",
                    RespondLogData = JObject.FromObject(respond).ToString(Formatting.None),
                    RequestLogData = JObject.FromObject(request).ToString(Formatting.None),
                    LogTime = DateTime.Now,
                    SpecialType = this.GetType(),
                    OpUserID = this.mSigninUser.UserID.ToString(),
                    OpUserName = this.mSigninUser.UserName
                };
                //由于电脑配置不上mongodb固暂时先屏蔽掉此段mongodb的数据操作
                //this.mMongoDbDataAccess.Save(mongo_db_request);
            });
            return respond;
        }

        public RespondWebViewData<RespondQueryDepartmentViewModel> GetDepartmentByComId(RequestWebViewData<RequestGetDepartmentByIdViewModel> request)
        {
            throw new NotImplementedException();
        }

        public RespondWebViewData<RequestEditDepartmentViewModel> EditDepartment(RequestWebViewData<RequestEditDepartmentViewModel> request)
        {
            throw new NotImplementedException();
        }

        public RespondWebViewData<RespondDeleteDepartmentViewModel> DeleteDepartment(RequestWebViewData<RequestDeleteDepartmentViewModel> request)
        {
            throw new NotImplementedException();
        }

        public RespondWebViewData<RespondDeleteDepartmentViewModel> ReDeleteDepartment(RequestWebViewData<RequestDeleteDepartmentViewModel> request)
        {
            throw new NotImplementedException();
        }
    }
}