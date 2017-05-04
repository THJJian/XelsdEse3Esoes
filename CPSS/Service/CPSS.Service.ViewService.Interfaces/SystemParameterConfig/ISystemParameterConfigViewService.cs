using System.Collections.Generic;
using CPSS.Common.Core;
using CPSS.Service.ViewService.ViewModels.SystemParameterConfig.Request;
using CPSS.Service.ViewService.ViewModels.SystemParameterConfig.Respond;

namespace CPSS.Service.ViewService.Interfaces.SystemParameterConfig
{
    public interface ISystemParameterConfigViewService
    {
        /// <summary>
        /// 获取系统配置参数
        /// </summary>
        /// <returns></returns>
        RespondWebViewData<List<RespondSystemParameterConfigViewModel>> GetSystemParameterConfigViewModels();

        /// <summary>
        /// 获取全局系统参数配置
        /// </summary>
        /// <returns></returns>
        Dictionary<string, RespondSystemParameterConfigViewModel> SystemParameterConfig();

        /// <summary>
        /// 获取指定系统配置参数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondSystemParameterConfigViewModel> GetSystemParameterConfigViewModel(RequestWebViewData<RequestSystemParameterConfigViewModel> request);

        /// <summary>
        /// 保存系统参数设置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondWebViewData<RespondSaveSystemParameterConfigViewModel> SaveSystemParameterConfig(RequestWebViewData<RequestSystemParameterConfigListViewModel> request);
    }
}