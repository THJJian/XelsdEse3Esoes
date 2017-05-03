using System;
using System.Collections.Generic;
using System.Linq;
using CPSS.Common.Core;
using CPSS.Common.Core.Exception;
using CPSS.Common.Core.Helper.Cached;
using CPSS.Common.Core.Helper.WebConfig;
using CPSS.Data.DataAccess.Interfaces.MongoDB;
using CPSS.Data.DataAccess.Interfaces.SystemParameterConfig;
using CPSS.Data.DataAccess.Interfaces.SystemParameterConfig.Parameters;
using CPSS.Service.ViewService.Interfaces.SystemParameterConfig;
using CPSS.Service.ViewService.ViewModels.SystemParameterConfig.Request;
using CPSS.Service.ViewService.ViewModels.SystemParameterConfig.Respond;

namespace CPSS.Service.ViewService.SystemParameterConfig
{
    public class SystemParameterConfigViewService : BaseViewService, ISystemParameterConfigViewService
    {
        private const string THISSERVICE_PRE_CACHE_KEY_MANAGE = "CPSS.Service.ViewService.SystemParameterConfig.SystemParameterConfigViewService";
        private const string PRE_CACHE_KEY = "CPSS.Service.ViewService.SystemParameterConfig.SystemParameterConfigViewService.{0}";

        private readonly ISystemParameterConfigDataAccess mSystemParameterConfigDataAccess;

        public SystemParameterConfigViewService(IMongoDbDataAccess mongoDbDataAccess, ISystemParameterConfigDataAccess _systemParameterConfigDataAccess) : base(mongoDbDataAccess)
        {
            this.mSystemParameterConfigDataAccess = _systemParameterConfigDataAccess;
        }

        public RespondWebViewData<List<RespondSystemParameterConfigViewModel>> GetSystemParameterConfigViewModels()
        {
            return MemcacheHelper.Get(new RequestMemcacheParameter<RespondWebViewData<List<RespondSystemParameterConfigViewModel>>>
            {
                CacheKey = string.Format(PRE_CACHE_KEY, "GetSystemParameterConfigViewModels"),

                #region ========================
                CallBackFunc = () =>
                {
                    var dataModels = this.mSystemParameterConfigDataAccess.GetSystemParameterConfigDataModels();
                    var viewModels = new RespondWebViewData<List<RespondSystemParameterConfigViewModel>>
                    {
                        rows = dataModels.Select(data => new RespondSystemParameterConfigViewModel
                        {
                            ParameterConfigName = data.ParameterConfigName,
                            ParameterConfigValue = data.ParameterConfigValue
                        }).ToList()
                    };
                    return viewModels;
                },
                #endregion
                
                ExpiresAt = DateTime.Now.AddMinutes(WebConfigHelper.MemCachedExpTime()),
                ManageCacheKeyForKey = THISSERVICE_PRE_CACHE_KEY_MANAGE,
                ParamsKeys = new object[]
                {
                    this.mSigninUser.UserID,
                    this.mSigninUser.CompanySerialNum
                }
            });
        }

        public RespondWebViewData<RespondSystemParameterConfigViewModel> GetSystemParameterConfigViewModel(RequestWebViewData<RequestSystemParameterConfigViewModel> request)
        {
            return MemcacheHelper.Get(new RequestMemcacheParameter<RespondWebViewData<RespondSystemParameterConfigViewModel>>
            {
                CacheKey = string.Format(PRE_CACHE_KEY, "GetSystemParameterConfigViewModel"),

                #region ===================================================
                CallBackFunc = () =>
                {
                    var parameter = new SystemParameterConfigParameter
                    {
                        ParameterConfigName = request.data.ParameterConfigName
                    };
                    var dataModel = this.mSystemParameterConfigDataAccess.GetSystemParameterConfigDataModel(parameter);
                    if (dataModel == null) return new RespondWebViewData<RespondSystemParameterConfigViewModel>(WebViewErrorCode.NotExistsDataInfo);
                    return new RespondWebViewData<RespondSystemParameterConfigViewModel>
                    {
                        rows = new RespondSystemParameterConfigViewModel
                        {
                            ParameterConfigName = dataModel.ParameterConfigName,
                            ParameterConfigValue = dataModel.ParameterConfigValue
                        }
                    };
                },
                #endregion

                ExpiresAt = DateTime.Now.AddMinutes(WebConfigHelper.MemCachedExpTime()),
                ManageCacheKeyForKey = THISSERVICE_PRE_CACHE_KEY_MANAGE,
                ParamsKeys = new object[]
                {
                    request.data.ParameterConfigName,
                    this.mSigninUser.UserID,
                    this.mSigninUser.CompanySerialNum
                }
            });
        }

        public RespondWebViewData<RespondSaveSystemParameterConfigViewModel> SaveSystemParameterConfig(RequestWebViewData<RequestSystemParameterConfigListViewModel> request)
        {
            var parameters = request.data.ParameterConfigList.Select(item => new SystemParameterConfigParameter
            {
                ParameterConfigName = item.ParameterConfigName,
                ParameterConfigValue = item.ParameterConfigValue
            }).ToList();
            var dataResult = this.mSystemParameterConfigDataAccess.SaveSystemParameterConfig(parameters);
            return new RespondWebViewData<RespondSaveSystemParameterConfigViewModel>(dataResult ? WebViewErrorCode.Success : WebViewErrorCode.Exception);
        }
    }
}