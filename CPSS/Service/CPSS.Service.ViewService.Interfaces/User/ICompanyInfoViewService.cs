using CPSS.Service.ViewService.ViewModels.User.Request;
using CPSS.Service.ViewService.ViewModels.User.Respond;

namespace CPSS.Service.ViewService.Interfaces.User
{
    public interface ICompanyInfoViewService
    {
        /// <summary>
        /// 根据用户公司ID获取用户库信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondCompanyInfoViewModel GetCompanyInfoViewModel(RequestCompanyInfoViewModel request);
    }
}