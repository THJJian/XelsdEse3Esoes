using System.Collections.Generic;
using CPSS.Service.ViewService.ViewModels.MainPage.Respond;

namespace CPSS.Service.ViewService.Interfaces.MainPage
{
    public interface ILeftNavMenuViewService
    {

        /// <summary>
        /// 获取用户菜单列表
        /// </summary>
        /// <param name="_userID"></param>
        /// <returns></returns>
        IList<RespondPanelViewModel> GetLeftNavMenuDataModels(int _userID);
    }
}