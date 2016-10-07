using System.Collections.Generic;
using System.Linq;
using CPSS.Data.DataAccess.Interfaces.HeadButtons;
using CPSS.Data.DataAccess.Interfaces.HeadButtons.Parameters;
using CPSS.Service.ViewService.Interfaces.HeadButtons;
using CPSS.Service.ViewService.ViewModels.HeadButtons.Request;
using CPSS.Service.ViewService.ViewModels.HeadButtons.Respond;

namespace CPSS.Service.ViewService.HeadButtons
{
    public class HeadButtonsViewService : IHeadButtonsViewService
    {
        private readonly IHeadButtonsDataAccess mHeadButtonsDataAccess;
        public HeadButtonsViewService(IHeadButtonsDataAccess _headButtonsDataAccess)
        {
            this.mHeadButtonsDataAccess = _headButtonsDataAccess;
        }

        public IList<RespondHeadButtonsViewModel> QueryHeadButtonsViewModelsByMenuID(RequestHeadButtonsViewModel request)
        {
            var parameter = new HeadButtonsParameter
            {
                MenuID = request.MenuID
            };
            var dataModels = this.mHeadButtonsDataAccess.QueryHeadButtonsViewModelsByMenuID(parameter);
            var viewModels = dataModels.Select(model => new RespondHeadButtonsViewModel
            {
                ButtonDisabled = model.ButtonDisabled,
                ButtonIconCls = model.ButtonIconCls,
                ButtonName = model.ButtonName,
                ButtonText = model.ButtonText
            }).ToList();
            return viewModels;
        }
    }
}