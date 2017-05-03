using System.Collections.Generic;

namespace CPSS.Service.ViewService.ViewModels.SystemParameterConfig.Request
{
    public class RequestSystemParameterConfigListViewModel
    {
        public RequestSystemParameterConfigListViewModel()
        {
            this.ParameterConfigList = new List<RequestSystemParameterConfigViewModel>();
        }

        public List<RequestSystemParameterConfigViewModel> ParameterConfigList { set; get; }
    }

    public class RequestSystemParameterConfigViewModel
    {
        public string ParameterConfigName { set; get; }

        public string ParameterConfigValue { set; get; }
    }
}