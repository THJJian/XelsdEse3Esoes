using CPSS.Common.Core.Helper.Extension;

namespace CPSS.Common.Core.Helper.Cached
{
    public class CacheKeyHelper
    {
        #region Public Methods and Operators

        /// <summary>
        ///     创建缓存key
        ///     如果isMD5为true或者长度超过阀值长度则进行Md5处理,如果都不满足则不进行处理
        /// </summary>
        /// <param name="sourceKey">CacheKey字符串</param>
        /// <param name="isMD5">是否执行Md5处理</param>
        /// <param name="lengthThreshold">长度阀值</param>
        /// <returns>缓存key</returns>
        public static string CreateCacheKeyWithMD5(string sourceKey, bool isMD5, int lengthThreshold)
        {
            var returnCacheKey = sourceKey;
            if (isMD5)
            {
                returnCacheKey = sourceKey.ToMD5String();
            }
            else
            {
                if (sourceKey.Length > lengthThreshold)
                {
                    returnCacheKey = sourceKey.ToMD5String();
                }
            }
            return returnCacheKey;
        }

        /// <summary>
        ///     创建缓存key
        ///     如果isMD5为true或者长度超过50则进行Md5处理,如果都不满足则不进行处理
        /// </summary>
        /// <param name="sourceKey">CacheKey字符串</param>
        /// <param name="isMD5">是否执行Md5处理</param>
        /// <returns>缓存key</returns>
        public static string CreateCacheKeyWithMD5(string sourceKey, bool isMD5)
        {
            return CreateCacheKeyWithMD5(sourceKey, isMD5, 50);
        }

        #endregion
    }
}