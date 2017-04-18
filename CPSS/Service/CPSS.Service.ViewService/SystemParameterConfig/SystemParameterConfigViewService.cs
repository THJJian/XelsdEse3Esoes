using System;
using System.Collections.Generic;
using System.Linq;
using CPSS.Common.Core;
using CPSS.Common.Core.Authenticate;
using CPSS.Common.Core.Helper.Cached;
using CPSS.Common.Core.Helper.WebConfig;
using CPSS.Data.DataAccess.Interfaces.SystemParameterConfig;
using CPSS.Data.DataAccess.Interfaces.SystemParameterConfig.Parameters;
using CPSS.Service.ViewService.Interfaces.SystemParameterConfig;
using CPSS.Service.ViewService.ViewModels.SystemParameterConfig.Request;
using CPSS.Service.ViewService.ViewModels.SystemParameterConfig.Respond;

namespace CPSS.Service.ViewService.SystemParameterConfig
{
    public class SystemParameterConfigViewService : ISystemParameterConfigViewService
    {
        private const string preCacheKey = "CPSS.Service.ViewService.SystemParameterConfig.SystemParameterConfigViewService.{0}";

        private readonly ISystemParameterConfigDataAccess mSystemParameterConfigDataAccess;
        
        private readonly SigninUser mSigninUser;

        public SystemParameterConfigViewService(ISystemParameterConfigDataAccess _systemParameterConfigDataAccess)
        {
            this.mSystemParameterConfigDataAccess = _systemParameterConfigDataAccess;
            this.mSigninUser = CPSSAuthenticate.GetCurrentUser();
        }

        public IList<RespondSystemParameterConfigViewModel> GetSystemParameterConfigViewModels()
        {
            return MemcacheHelper.Get(() =>
                {
                    var dataModels = this.mSystemParameterConfigDataAccess.GetSystemParameterConfigDataModels();
                    var viewModels = dataModels.Select(data => new RespondSystemParameterConfigViewModel
                    {
                        ParameterConfigName = data.ParameterConfigName,
                        ParameterConfigValue = data.ParameterConfigValue
                    }).ToList();
                    return viewModels;
                }, string.Format(preCacheKey, "GetSystemParameterConfigViewModels"),
                DateTime.Now.AddMinutes(WebConfigHelper.MemCachedExpTime()),
                false,
                this.mSigninUser.UserID,
                this.mSigninUser.CompanySerialNum);
        }

        public RespondSystemParameterConfigViewModel GetSystemParameterConfigViewModel(RequestSystemParameterConfigViewModel request)
        {
            return MemcacheHelper.Get(() =>
                {
                    var parameter = new SystemParameterConfigParameter
                    {
                        ParameterConfigName = request.ParameterConfigName
                    };
                    var dataModel = this.mSystemParameterConfigDataAccess.GetSystemParameterConfigDataModel(parameter);
                    if(dataModel == null) return new RespondSystemParameterConfigViewModel();
                    return new RespondSystemParameterConfigViewModel
                    {
                        ParameterConfigName = dataModel.ParameterConfigName,
                        ParameterConfigValue = dataModel.ParameterConfigValue
                    };
                },
                string.Format(preCacheKey, "GetSystemParameterConfigViewModel"),
                false,
                request.ParameterConfigName,
                this.mSigninUser.UserID,
                this.mSigninUser.CompanySerialNum);
        }
    }
}