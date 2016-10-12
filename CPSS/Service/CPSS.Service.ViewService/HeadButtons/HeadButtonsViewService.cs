using System;
using System.Collections.Generic;
using System.Linq;
using CPSS.Common.Core.Helper.Cached;
using CPSS.Common.Core.Helper.WebConfig;
using CPSS.Data.DataAccess.Interfaces.HeadButtons;
using CPSS.Data.DataAccess.Interfaces.HeadButtons.Parameters;
using CPSS.Service.ViewService.Interfaces.HeadButtons;
using CPSS.Service.ViewService.ViewModels.HeadButtons.Request;
using CPSS.Service.ViewService.ViewModels.HeadButtons.Respond;

namespace CPSS.Service.ViewService.HeadButtons
{
    public class HeadButtonsViewService : IHeadButtonsViewService
    {
        private const string preCacheKey = "CPSS.Service.ViewService.HeadButtons.HeadButtonsViewService.{0}";
        private readonly IHeadButtonsDataAccess mHeadButtonsDataAccess;
        public HeadButtonsViewService(IHeadButtonsDataAccess _headButtonsDataAccess)
        {
            this.mHeadButtonsDataAccess = _headButtonsDataAccess;
        }

        public IList<RespondHeadButtonsViewModel> QueryHeadButtonsViewModelsByMenuID(RequestHeadButtonsViewModel request)
        {
            return MemcacheHelper.Get(() =>
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
            }, 
            string.Format(preCacheKey, "QueryHeadButtonsViewModelsByMenuID"), 
            DateTime.Now.AddMinutes(WebConfigHelper.MemCachedExpTime()), 
            request.MenuID, 
            request.ClassID);
        }
    }
}