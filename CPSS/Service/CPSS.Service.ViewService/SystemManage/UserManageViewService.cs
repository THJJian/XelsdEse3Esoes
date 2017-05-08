using System;
using System.Collections.Generic;
using System.Linq;
using CPSS.Common.Core;
using CPSS.Common.Core.Exception;
using CPSS.Common.Core.Helper.Cached;
using CPSS.Common.Core.Type.ConstDefined;
using CPSS.Data.DataAccess.Interfaces.MongoDB;
using CPSS.Data.DataAccess.Interfaces.SystemManage.UserManage;
using CPSS.Data.DataAccess.Interfaces.SystemManage.UserManage.Parameters;
using CPSS.Service.ViewService.Interfaces.SystemManage.UserManage;
using CPSS.Service.ViewService.ViewModels.SystemManage.UserManage.Request;
using CPSS.Service.ViewService.ViewModels.SystemManage.UserManage.Respond;

namespace CPSS.Service.ViewService.SystemManage
{
    public class UserManageViewService : BaseViewService, IUserManageViewService
    {
        private const string PRE_CACHE_KEY = "CPSS.Service.ViewService.SystemManage.UserManageViewService.{0}";
        private readonly IUserDataAccess mUserDataAccess;

        public UserManageViewService(IMongoDbDataAccess mongoDbDataAccess, IUserDataAccess userDataAccess) : base(mongoDbDataAccess)
        {
            this.mUserDataAccess = userDataAccess;
        }

        public RespondWebViewData<List<RespondQueryUserViewModel>> GetUserList(RequestWebViewData<RequestQueryUserViewModel> request)
        {
            return MemcacheHelper.Get(new RequestMemcacheParameter<RespondWebViewData<List<RespondQueryUserViewModel>>>
            {
                CacheKey = string.Format(PRE_CACHE_KEY, "GetUserList"),

                #region ============================================

                CallBackFunc = () =>
                {
                    var parameter = new QueryUserParameter
                    {
                        EmpId = request.data.EmpId,
                        PageIndex = request.page,
                        PageSize = request.rows,
                        UserName = request.data.UserName
                    };
                    var pageList = this.mUserDataAccess.GetUserPageData(parameter);
                    var respond = new RespondWebViewData<List<RespondQueryUserViewModel>>
                    {
                        total = pageList.DataCount,
                        rows = pageList.Datas.Select(item => new RespondQueryUserViewModel
                        {
                            CTime = item.CTime,
                            Comment = item.Comment,
                            EmpId = item.EmpId,
                            Manager = item.Manager,
                            Prefix = item.Prefix,
                            Status = item.Status,
                            UserName = item.UserName,
                            UsePwd = item.UsePwd,
                            UserId = item.UserId,
                            Deleted = item.Deleted,
                            Synchron = item.Synchron,
                            ComId = item.ComId
                        }).ToList()
                    };
                    return respond;
                },

                #endregion
                
                ExpiresAt = DateTime.Now.AddMinutes(30),
                ManageCacheKeyForKey = ServiceMemcachedKeyManageConst.UserManage,
                ParamsKeys = new object[]
                {
                    request.data.EmpId,
                    request.page,
                    request.rows,
                    request.data.UserName
                }
            });
        }

        public RespondWebViewData<RespondQueryUserViewModel> GetUserDataByUserId(RequestWebViewData<RequestQueryUserViewModel> request)
        {
            return MemcacheHelper.Get(new RequestMemcacheParameter<RespondWebViewData<RespondQueryUserViewModel>>
            {
                CacheKey = string.Format(PRE_CACHE_KEY, "GetUserDataByUserId"),

                #region ========================================================

                CallBackFunc = () =>
                {
                    var parameter = new QueryUserParameter
                    {
                        userid = request.data.UserId
                    };
                    var dataModel = this.mUserDataAccess.GetUserDataByUserId(parameter);
                    if (dataModel == null) return new RespondWebViewData<RespondQueryUserViewModel>(WebViewErrorCode.NotExistsDataInfo);
                    var respond = new RespondWebViewData<RespondQueryUserViewModel>
                    {
                        rows = new RespondQueryUserViewModel
                        {
                            CTime = dataModel.CTime,
                            Comment = dataModel.Comment,
                            EmpId = dataModel.EmpId,
                            Manager = dataModel.Manager,
                            Prefix = dataModel.Prefix,
                            Status = dataModel.Status,
                            UserName = dataModel.UserName,
                            UsePwd = dataModel.UsePwd,
                            UserId = dataModel.UserId,
                            Deleted = dataModel.Deleted,
                            Synchron = dataModel.Synchron,
                            ComId = dataModel.ComId
                        }
                    };
                    return respond;
                },

                #endregion
            
                ExpiresAt = DateTime.Now.AddMinutes(30),
                ManageCacheKeyForKey = ServiceMemcachedKeyManageConst.UserManage,
                ParamsKeys = new object[]
                {
                    request.data.UserId
                }
            });
        }
    }
}