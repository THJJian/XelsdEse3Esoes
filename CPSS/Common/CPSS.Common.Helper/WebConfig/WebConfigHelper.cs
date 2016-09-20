using System.Configuration;

namespace CPSS.Common.Core.Helper.WebConfig
{
    public class WebConfigHelper
    {
        /// <summary>
        /// 获取资源配置站点域名 http://i.domain.net
        /// </summary>
        /// <param name="key"></param>
        /// <returns>http://i.domain.net</returns>
        public static string ResourceDomain(string key = "CSPP.Resource.Domain")
        {
            var value = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(value)) value = "http://i.cspp.net";
            return value;
        }

    }
}