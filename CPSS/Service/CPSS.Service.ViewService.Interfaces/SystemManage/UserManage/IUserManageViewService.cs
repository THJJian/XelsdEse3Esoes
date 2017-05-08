using System.Collections.Generic;
using CPSS.Common.Core;
using CPSS.Service.ViewService.ViewModels.SystemManage.UserManage.Request;
using CPSS.Service.ViewService.ViewModels.SystemManage.UserManage.Respond;

namespace CPSS.Service.ViewService.Interfaces.SystemManage.UserManage
{
    public interface IUserManageViewService
    {
        /// <summary>
        /// 获取用户列表信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<List<RespondQueryUserViewModel> > GetUserList(RequestWebViewData<RequestQueryUserViewModel> request);

        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondQueryUserViewModel> GetUserDataByUserId(RequestWebViewData<RequestQueryUserViewModel> request);
    }
}