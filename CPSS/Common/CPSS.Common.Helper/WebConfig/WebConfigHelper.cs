using System.Configuration;
using CPSS.Common.Core.Helper.Extension;

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

        /// <summary>
        /// 获取系统配置的缓存过期时间(单位为[分])，默认为10分钟过期
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int MemCachedExpTime(string key = "CSPP.MemCached.ExpTime")
        {
            var _value = 10;
            var value = ConfigurationManager.AppSettings[key];
            if (!string.IsNullOrEmpty(value)) _value = value.ToInt32();
            return _value;
        }

        /// <summary>
        /// 获取MongoDb配置的服务器地址
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetMongoDbServer(string key = "CSPP.MongoDb.Server.Address")
        {
            var value = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(value)) value = "mongodb://127.0.0.1";
            return value;
        }
    }
}