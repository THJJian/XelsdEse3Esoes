using System.Collections.Generic;
using System.Linq;
using CPSS.Common.Core.Authenticate;
using CPSS.Data.DataAccess.Interfaces.MainPage;
using CPSS.Data.DataAccess.Interfaces.MainPage.Parameters;
using CPSS.Service.ViewService.Interfaces.MainPage;
using CPSS.Service.ViewService.ViewModels.MainPage.Respond;

namespace CPSS.Service.ViewService.MainPage
{
    public class LeftNavMenuViewService : ILeftNavMenuViewService
    {
        private readonly ILeftNavMenuDataAccess mLeftNavMenuDataAccess;

        public LeftNavMenuViewService(ILeftNavMenuDataAccess _leftNavMenuDataAccess)
        {
            this.mLeftNavMenuDataAccess = _leftNavMenuDataAccess;
        }

        public IList<RespondPanelViewModel> GetLeftNavMenuDataModels()
        {
            var user = CPSSAuthenticate.GetCurrentUser();
            var parameter = new LeftNavMenuParameter
            {
                UserID = user.UserID
            };
            var dataModels = this.mLeftNavMenuDataAccess.GetLeftNavMenuDataModels(parameter);
            var viewModels = dataModels
                .Where(dataModel => dataModel.parentid == "000001")
                .Select(dataModel => new RespondPanelViewModel
                {
                    IconCls = dataModel.iconcls,
                    Title = dataModel.title,
                    Menus = dataModels
                        .Where(_dataModel=>_dataModel.parentid == dataModel.classid)
                        .Select(_dataModel=>new RespondMenuViewModel
                        {
                            IconCls = _dataModel.iconcls,
                            Title = _dataModel.title,
                            Url = _dataModel.url,
                            ButtonID = _dataModel.buttonid,
                            MenuID = _dataModel.menuid,
                            ClassID = _dataModel.classid,
                        })
                        .ToList()
                }).ToList();
            return viewModels;
        }
    }
}