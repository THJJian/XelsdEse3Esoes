using System;

namespace CPSS.Common.Core.Helper.Cached
{
    public class RequestMemcacheParameter<T> where T : class, new()
    {
        public RequestMemcacheParameter()
        {
            this.ParamsKeys = new object[]{};
        }

        public Func<T> CallBackFunc { set; get; }

        public string CacheKey { set; get; }

        public DateTime ExpiresAt { set; get; }

        public object[] ParamsKeys { set; get; }
        
        /// <summary>
        /// 管理缓存键的缓存键
        /// </summary>
        public string ManageCacheKeyForKey { set; get; }
    }
}