using System;
using CPSS.Common.Core;
using CPSS.Data.DataAccess.Interfaces.MongoDB;
using CPSS.Service.ViewService.Interfaces.SystemManage.UserManage;
using CPSS.Service.ViewService.ViewModels.SystemManage.UserManage.Request;
using CPSS.Service.ViewService.ViewModels.SystemManage.UserManage.Respond;

namespace CPSS.Service.ViewService.SystemManage
{
    public class UserManageViewService : BaseViewService, IUserManageViewService
    {
        public UserManageViewService(IMongoDbDataAccess mongoDbDataAccess) : base(mongoDbDataAccess)
        {
        }


        public RespondWebViewData<RespondQueryUserViewModel> GetUserList(RequestWebViewData<RequestQueryUserViewModel> request)
        {
            throw new NotImplementedException();
        }
    }
}