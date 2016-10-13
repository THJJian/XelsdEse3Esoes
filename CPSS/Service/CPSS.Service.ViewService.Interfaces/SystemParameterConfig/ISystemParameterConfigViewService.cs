using System.Collections.Generic;
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
        IList<RespondSystemParameterConfigViewModel> GetSystemParameterConfigViewModels();

        /// <summary>
        /// 获取指定系统配置参数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        RespondSystemParameterConfigViewModel GetSystemParameterConfigViewModel(RequestSystemParameterConfigViewModel request);
    }
}