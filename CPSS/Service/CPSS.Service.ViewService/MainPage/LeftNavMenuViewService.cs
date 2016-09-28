using System.Collections.Generic;
using System.Linq;
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

        public IList<RespondPanelViewModel> GetLeftNavMenuDataModels(int _userID)
        {
            var parameter = new LeftNavMenuParameter
            {
                UserID = _userID
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
                            Url = _dataModel.Url
                        })
                        .ToList()
                }).ToList();
            return viewModels;
        }
    }
}