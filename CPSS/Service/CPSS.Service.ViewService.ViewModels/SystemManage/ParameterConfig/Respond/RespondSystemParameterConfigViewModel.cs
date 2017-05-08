using System;

namespace CPSS.Service.ViewService.ViewModels.SystemParameterConfig.Respond
{
    [Serializable]
    public class RespondSystemParameterConfigViewModel
    {
        /// <summary>
        /// 配置控件ID
        /// </summary>
        public string ParameterConfigName { set; get; }

        /// <summary>
        /// 配置控件的值
        /// </summary>
        public string ParameterConfigValue { set; get; }
    }
}