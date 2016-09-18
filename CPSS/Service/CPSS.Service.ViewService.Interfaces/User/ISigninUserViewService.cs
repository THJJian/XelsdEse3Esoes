using CPSS.Common.Core.Exception;
using CPSS.Service.ViewService.ViewModels.User.Request;
using CPSS.Service.ViewService.ViewModels.User.Respond;

namespace CPSS.Service.ViewService.Interfaces.User
{
    public interface ISigninUserViewService
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondSigninUserViewModel> QuerySigninUserViewModel(RequestSigninUserViewModel request);

        /// <summary>
        /// 将登陆用户保存至在线列表内
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool SaveLoginUserToOnline(RequestSigninUserViewModel request);

        /// <summary>
        /// 根据UserID_g获取在线用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondOnlineSigninUserViewModel> GetOnlineSigninUserByUserID_g(RequestOnlineSigninUserViewModel request);

        /// <summary>
        /// 根据UserID获取用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondSigninUserViewModel> FindSininUserDataModelByUserID(RequestOnlineSigninUserViewModel request);
    }
}