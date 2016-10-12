using System;
using CPSS.Common.Core.Authenticate;
using CPSS.Common.Core.Helper.Cached;
using CPSS.Common.Core.Helper.WebConfig;
using CPSS.Data.DataAccess.Interfaces.MenuRight;
using CPSS.Data.DataAccess.Interfaces.MenuRight.Parameters;
using CPSS.Service.ViewService.Interfaces.MenuRight;
using CPSS.Service.ViewService.ViewModels.MenuRight.Request;

namespace CPSS.Service.ViewService.MenuRight
{
    public class MenuRightCheckViewService :IMenuRightCheckViewService
    {
        private const string preCacheKey = "CPSS.Service.ViewService.MenuRight.MenuRightCheckViewService.{0}";
        private readonly IMenuRightCheckDataAccess mMenuRightCheckDataAccess;

        public MenuRightCheckViewService(IMenuRightCheckDataAccess _menuRightCheckDataAccess)
        {
            this.mMenuRightCheckDataAccess = _menuRightCheckDataAccess;
        }

        public bool CheckMenuRightByMenuID(RequestMenuRightCheckViewModel request)
        {
            var user = CPSSAuthenticate.GetCurrentUser();
            return MemcacheHelper.Get(() =>
                {
                    var parameter = new MenuRightCheckParameter
                    {
                        MenuID = request.MenuID,
                        UserID = user.UserID
                    };
                    var dataModel = this.mMenuRightCheckDataAccess.CheckMenuRightByMenuID(parameter);
                    return dataModel != null && dataModel.HaveRight;
                }, string.Format(preCacheKey, "CheckMenuRightByMenuID")
                , DateTime.Now.AddMinutes(WebConfigHelper.MemCachedExpTime())
                , request.MenuID
                , user.UserID);
        }
    }
}