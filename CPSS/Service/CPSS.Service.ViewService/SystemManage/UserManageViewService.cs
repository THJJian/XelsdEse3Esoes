using System;
using System.Collections.Generic;
using System.Linq;
using CPSS.Common.Core;
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
        private readonly IUserDataAccess mUserDataAccess;

        public UserManageViewService(IMongoDbDataAccess mongoDbDataAccess, IUserDataAccess userDataAccess) : base(mongoDbDataAccess)
        {
            this.mUserDataAccess = userDataAccess;
        }


        public RespondWebViewData<List<RespondQueryUserViewModel>> GetUserList(RequestWebViewData<RequestQueryUserViewModel> request)
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
                rows = pageList.Datas.Select(item=> new RespondQueryUserViewModel
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
        }
    }
}