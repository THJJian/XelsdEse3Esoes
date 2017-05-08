using System.Collections.Generic;
using System.Linq;
using CPSS.Common.Core;
using CPSS.Common.Core.Exception;
using CPSS.Common.Core.Helper.Cached;
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
                    var kvConfig = this.BuildDictionay();
                    var value = kvConfig[request.data.ParameterConfigName];
                    return  new RespondWebViewData<RespondSystemParameterConfigViewModel>
                    {
                        rows = value
                    };
                },
                #endregion

                ManageCacheKeyForKey = THISSERVICE_PRE_CACHE_KEY_MANAGE,
                ParamsKeys = new object[]
                {
                    request.data.ParameterConfigName,
                    this.mSigninUser.CompanySerialNum
                }
            });
        }

        /// <summary>
        /// 把系统配置参数缓存为键值对的格式方便使用
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, RespondSystemParameterConfigViewModel> BuildDictionay()
        {
            return MemcacheHelper.Get(new RequestMemcacheParameter<Dictionary<string, RespondSystemParameterConfigViewModel>>
            {
                CacheKey = string.Format(PRE_CACHE_KEY, "BuildDictionay"),

                #region =============================================
                CallBackFunc = () =>
                {
                    var configList = this.GetSystemParameterConfigViewModels();
                    var kvConfig = configList.rows.ToDictionary(item => item.ParameterConfigName, item => new RespondSystemParameterConfigViewModel{ ParameterConfigName = item.ParameterConfigName, ParameterConfigValue = item.ParameterConfigValue});
                    return kvConfig;
                },
                #endregion
                ManageCacheKeyForKey = THISSERVICE_PRE_CACHE_KEY_MANAGE,
                ParamsKeys = new object[]
                {
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
            if (dataResult) MemcacheHelper.RemoveBy(THISSERVICE_PRE_CACHE_KEY_MANAGE);
            return new RespondWebViewData<RespondSaveSystemParameterConfigViewModel>(dataResult ? WebViewErrorCode.Success : WebViewErrorCode.Exception);
        }

        public Dictionary<string, RespondSystemParameterConfigViewModel> SystemParameterConfig()
        {
            return this.BuildDictionay();
        }
    }
}