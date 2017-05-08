using System.Collections.Generic;
using CPSS.Common.Core;
using CPSS.Service.ViewService.ViewModels.SystemManage.UserManage.Request;
using CPSS.Service.ViewService.ViewModels.SystemManage.UserManage.Respond;

namespace CPSS.Service.ViewService.Interfaces.SystemManage.UserManage
{
    public interface IUserManageViewService
    {
        RespondWebViewData<List<RespondQueryUserViewModel> > GetUserList(RequestWebViewData<RequestQueryUserViewModel> request);
    }
}