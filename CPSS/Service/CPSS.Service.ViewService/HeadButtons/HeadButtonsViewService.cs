using System;
using System.Collections.Generic;
using System.Linq;
using CPSS.Common.Core.Helper.Cached;
using CPSS.Common.Core.Helper.WebConfig;
using CPSS.Data.DataAccess.Interfaces.HeadButtons;
using CPSS.Data.DataAccess.Interfaces.HeadButtons.Parameters;
using CPSS.Data.DataAccess.Interfaces.MongoDB;
using CPSS.Service.ViewService.Interfaces.HeadButtons;
using CPSS.Service.ViewService.ViewModels.HeadButtons.Request;
using CPSS.Service.ViewService.ViewModels.HeadButtons.Respond;

namespace CPSS.Service.ViewService.HeadButtons
{
    public class HeadButtonsViewService : BaseViewService, IHeadButtonsViewService
    {
        private const string THISSERVICE_PRE_CACHE_KEY_MANAGE = "CPSS.Service.ViewService.HeadButtons.HeadButtonsViewService";
        private const string PRE_CACHE_KEY = "CPSS.Service.ViewService.HeadButtons.HeadButtonsViewService.{0}";

        private readonly IHeadButtonsDataAccess mHeadButtonsDataAccess;
        public HeadButtonsViewService(IMongoDbDataAccess mongoDbDataAccess, IHeadButtonsDataAccess _headButtonsDataAccess) : base(mongoDbDataAccess)
        {
            this.mHeadButtonsDataAccess = _headButtonsDataAccess;
        }

        public List<RespondHeadButtonsViewModel> QueryHeadButtonsViewModelsByMenuID(RequestHeadButtonsViewModel request)
        {
            //TODO 配置每个单据的按钮权限后需清除缓存
            return MemcacheHelper.Get(new RequestMemcacheParameter<List<RespondHeadButtonsViewModel>>
            {
                CacheKey = string.Format(PRE_CACHE_KEY, "QueryHeadButtonsViewModelsByMenuID"),

                #region ====================================
                CallBackFunc = () =>
                {
                    var parameter = new HeadButtonsParameter
                    {
                        MenuID = request.MenuID,
                        UserId = this.mSigninUser.UserID
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
                #endregion
                ExpiresAt = DateTime.Now.AddMinutes(WebConfigHelper.MemCachedExpTime()),
                ManageCacheKeyForKey = THISSERVICE_PRE_CACHE_KEY_MANAGE,
                ParamsKeys = new object[]
                {
                    request.MenuID,
                    this.mSigninUser.UserID
                }
            });
        }
    }
}