using System;
using System.Collections.Generic;
using Enyim.Caching;
using Enyim.Caching.Memcached;

namespace CPSS.Common.Core.Helper.Cached
{
    public class MemcacheHelper
    {
        private static readonly DateTime NotExpirersTime = DateTime.Now.AddHours(24);
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
        /// 根据真实缓存键移除当前服务的所有缓存
        /// </summary>
        /// <param name="cacheKey"></param>
        public static void RemoveBy(string cacheKey)
        {
            var allCacheKeys = Get(cacheKey);
            allCacheKeys.Add(cacheKey);
            foreach (var realCacheKey in allCacheKeys)
            {
                RemoveCache(realCacheKey);
            }
        }

        /// <summary>
        ///     从缓存中获取指定项的缓存数据，如果不存在指定项缓存的数据，将执行缓存本次的数据缓存
        /// </summary>
        /// <param name="callBack">如果缓存未命中取数据的方法</param>
        /// <param name="cacheKey"></param>
        /// <param name="expiresAt"></param>
        /// <param name="isRealCacheKey"></param>
        /// <param name="keys"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T>(Func<T> callBack, string cacheKey, DateTime expiresAt, bool isRealCacheKey, params object[] keys)
        {
            if (!isRealCacheKey)
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
        /// <param name="isRealCacheKey"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T>(Func<T> callBack, string cacheKey, bool isRealCacheKey, params object[] keys)
        {
            return Get(callBack, cacheKey, NotExpirersTime, isRealCacheKey, keys);
        }

        /// <summary>
        /// 需要管理缓存键的服务类调用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static T Get<T>(RequestMemcacheParameter<T> parameter) where T: class, new()
        {
            var realCacheKey = GetRealCacheKey(parameter.CacheKey, parameter.ParamsKeys);
            if (!string.IsNullOrEmpty(parameter.ManageCacheKeyForKey))
                Update(parameter.ManageCacheKeyForKey, realCacheKey);
            return Get(parameter.CallBackFunc, realCacheKey, parameter.ExpiresAt.HasValue ? parameter.ExpiresAt.Value : NotExpirersTime, true, parameter.ParamsKeys);
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
        
        #region 服务类的缓存的缓存键管理缓存

        /// <summary>
        /// 只用于管理服务类的缓存的缓存键。
        /// 返回值永远不可能为空，使用时不需要做空判断。
        /// </summary>
        /// <param name="cacheKey">管理当前服务类的缓存的缓存键的缓存键</param>
        /// <returns></returns>
        public static List<string> Get(string cacheKey)
        {
            var cacheddata = GetCache(cacheKey);
            if (cacheddata != null)
            {
                return cacheddata as List<string>;
            }
            lock (GetLockObject(cacheKey))
            {
                cacheddata = GetCache(cacheKey);
                if (cacheddata != null)
                {
                    return cacheddata as List<string>;
                }
                return new List<string>();
            }
        }

        /// <summary>
        /// 更新当前服务类的缓存键管理的缓存的缓存键
        /// </summary>
        /// <param name="manageCacheKeyForKey">管理当前服务类的缓存的缓存键的缓存键</param>
        /// <param name="newCacheKey">当前服务类的新缓存键</param>
        private static void Update(string manageCacheKeyForKey, string newCacheKey)
        {
            var allKeys = Get(manageCacheKeyForKey);
            if (allKeys.Contains(newCacheKey)) return;
            allKeys.Add(newCacheKey);
            SetCache(manageCacheKeyForKey, allKeys, NotExpirersTime, StoreMode.Set);
        }

        #endregion
        
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
            result = new CacheLockObject(new object().GetHashCode());
            SetCache(cacheKey, result, NotExpirersTime);
            return result;
        }

        private static string GetRealCacheKey(string cacheKey, params object[] keys)
        {
            var cacheKeyParm = string.Join("/", keys);
            cacheKey = CacheKeyHelper.CreateCacheKeyWithMD5(cacheKey + cacheKeyParm, true);
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