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
                .Where(dataModel => dataModel.ParentClassID == "000001")
                .Select(dataModel => new RespondPanelViewModel
                {
                    IconCls = dataModel.IconCls,
                    Title = dataModel.Title,
                    Menus = dataModels
                        .Where(_dataModel=>_dataModel.ParentClassID == dataModel.ClassID)
                        .Select(_dataModel=>new RespondMenuViewModel
                        {
                            IconCls = _dataModel.IconCls,
                            Title = _dataModel.Title,
                            Url = _dataModel.Url,
                            ButtonID = _dataModel.ButtonID,
                            MenuID = _dataModel.MenuID
                        })
                        .ToList()
                }).ToList();
            return viewModels;
        }
    }
}