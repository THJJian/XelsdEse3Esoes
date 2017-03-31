using System;
using Enyim.Caching;
using Enyim.Caching.Memcached;

namespace CPSS.Common.Core.Helper.Cached
{
    public class MemcacheHelper
    {
        private static readonly DateTime NotExpirersTime = DateTime.Now.AddDays(15);
        private static readonly MemcachedClient EnyimMemcachedClient = new MemcachedClient("EnyimMemcached/memcached");

        /// <summary>
        ///     从缓存中删除指定项
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="keys"></param>
        public static void Remove(string cacheKey, params object[] keys)
        {
            cacheKey = GetRealCacheKey(cacheKey, keys);
            RemoveCache(cacheKey);
        }

        /// <summary>
        ///     从缓存中获取指定项的缓存数据，如果不存在指定项缓存的数据，将执行缓存本次的数据缓存
        /// </summary>
        /// <param name="callBack">如果缓存未命中取数据的方法</param>
        /// <param name="cacheKey"></param>
        /// <param name="expiresAt"></param>
        /// <param name="keys"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T>(Func<T> callBack, string cacheKey, DateTime expiresAt, params object[] keys)
        {
            cacheKey = GetRealCacheKey(cacheKey, keys);
            var cacheddata = GetCache(cacheKey);
            if (cacheddata != null)
            {
                return (T)cacheddata;
            }
            lock (GetLockObject(cacheKey))
            {
                cacheddata = GetCache(cacheKey);
                if (cacheddata != null)
                {
                    return (T)cacheddata;
                }
                var value = callBack();
                SetCache(cacheKey, value, expiresAt);
                return value;
            }
        }

        /// <summary>
        ///     从缓存中获取指定项的缓存数据，如果不存在指定项缓存的数据，将执行缓存本次的数据缓存(永不过期)
        /// </summary>
        /// <param name="callBack">如果缓存未命中取数据的方法</param>
        /// <param name="cacheKey"></param>
        /// <param name="keys"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T>(Func<T> callBack, string cacheKey, params object[] keys)
        {
            return Get(callBack, cacheKey, NotExpirersTime, keys);
        }

        /// <summary>
        ///     更新缓存中指定项的缓存数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callBack"></param>
        /// <param name="cacheKey"></param>
        /// <param name="expiresAt"></param>
        /// <param name="IsNotExpired">是否永不过期</param>
        /// <param name="keys"></param>
        public static T Update<T>(Func<T> callBack, string cacheKey, DateTime expiresAt, bool IsNotExpired, params object[] keys)
        {
            cacheKey = GetRealCacheKey(cacheKey, keys);
            var value = callBack();
            SetCache(cacheKey, value, IsNotExpired ? NotExpirersTime : expiresAt, StoreMode.Set);
            return value;
        }

        #region 私有方法

        private static object GetCache(string cacheKey)
        {
            return EnyimMemcachedClient.Get(cacheKey);
        }

        private static void RemoveCache(string cacheKey)
        {
            EnyimMemcachedClient.Remove(cacheKey);
        }

        private static void SetCache(string cacheKey, object value, DateTime expiresAt, StoreMode mode = StoreMode.Add)
        {
#if DEBUG
            var success = EnyimMemcachedClient.ExecuteStore(mode, cacheKey, value, expiresAt);
            if (success.Success) return;
            var message = success.Exception != null ? success.Exception.Message : success.Message;
            throw new Exception(string.Concat("写入缓存失败", cacheKey, " ", message));
#else
            EnyimMemcachedClient.Store(mode, cacheKey, value, expiresAt);
#endif
        }

        /// <summary>
        ///     缓存锁
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        private static object GetLockObject(string key)
        {
            var cacheKey = string.Format("$SynchronizedCacheHelperLockObject${0}", key);
            var result = GetCache(cacheKey);
            if (result != null)
            {
                return result;
            }
            result = new CacheLockObject(new Object().GetHashCode());
            SetCache(cacheKey, result, NotExpirersTime);
            return result;
        }

        private static string GetRealCacheKey(string cacheKey, params object[] keys)
        {
            var cacheKeyParm = string.Join("/", keys);
            cacheKey = cacheKeyParm.Length + cacheKey.Length > 50
                ? CacheKeyHelper.CreateCacheKeyWithMD5(cacheKey + cacheKeyParm, true)
                : string.Format("#{0}/{1}", cacheKey, cacheKeyParm);
            return cacheKey;
        }

        #endregion 私有方法
    }

    [Serializable]
    public class CacheLockObject
    {
        public CacheLockObject(int hashCode)
        {
            this.HashCode = hashCode;
        }

        public int HashCode { get; set; }

        public override int GetHashCode()
        {
            return this.HashCode;
        }
    }
}