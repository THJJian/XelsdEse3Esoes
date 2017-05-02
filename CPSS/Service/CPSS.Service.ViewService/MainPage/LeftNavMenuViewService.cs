using System;
using System.Collections.Generic;
using System.Linq;
using CPSS.Common.Core.Helper.Cached;
using CPSS.Data.DataAccess.Interfaces.MainPage;
using CPSS.Data.DataAccess.Interfaces.MainPage.Parameters;
using CPSS.Data.DataAccess.Interfaces.MongoDB;
using CPSS.Service.ViewService.Interfaces.MainPage;
using CPSS.Service.ViewService.ViewModels.MainPage.Respond;

namespace CPSS.Service.ViewService.MainPage
{
    public class LeftNavMenuViewService : BaseViewService, ILeftNavMenuViewService
    {
        private const string THISSERVICE_PRE_CACHE_KEY_MANAGE = "CPSS.Service.ViewService.MainPage.LeftNavMenuViewService";
        private const string PRE_CACHE_KEY = "CPSS.Service.ViewService.MainPage.LeftNavMenuViewService.{0}";
        private readonly ILeftNavMenuDataAccess mLeftNavMenuDataAccess;

        public LeftNavMenuViewService(IMongoDbDataAccess mongoDbDataAccess, ILeftNavMenuDataAccess _leftNavMenuDataAccess) : base(mongoDbDataAccess)
        {
            this.mLeftNavMenuDataAccess = _leftNavMenuDataAccess;
        }

        public List<RespondPanelViewModel> GetLeftNavMenuDataModels()
        {
            var parameter = new LeftNavMenuParameter
            {
                UserID = this.mSigninUser.UserID
            };

            //TODO 配置菜单权限是需要清除当前的用户的菜单缓存
            return MemcacheHelper.Get(new RequestMemcacheParameter<List<RespondPanelViewModel>>
            {
                CacheKey = string.Format(PRE_CACHE_KEY, "GetLeftNavMenuDataModels"),

                #region ===============================
                CallBackFunc = () =>
                {
                    var dataModels = this.mLeftNavMenuDataAccess.GetLeftNavMenuDataModels(parameter);
                    var viewModels = dataModels
                        .Where(dataModel => dataModel.parentid == "000001")
                        .Select(dataModel => new RespondPanelViewModel
                        {
                            IconCls = dataModel.iconcls,
                            Title = dataModel.title,
                            Menus = dataModels
                                .Where(_dataModel => _dataModel.parentid == dataModel.classid)
                                .Select(_dataModel => new RespondMenuViewModel
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
                },
                #endregion
                ExpiresAt = DateTime.Now.AddMinutes(30),
                ManageCacheKeyForKey = THISSERVICE_PRE_CACHE_KEY_MANAGE,
                ParamsKeys = new object[]
                {
                    this.mSigninUser.UserID
                }
            });
            
        }
    }
}