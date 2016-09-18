using System.Linq;
using CPSS.Common.Core.Helper.Extension;

namespace CPSS.Common.Core.Helper.Cached
{
    public class CacheKeyHelper
    {
        #region Methods

        /// <summary>
        ///     获取MD5加密字符串
        /// </summary>
        /// <param name="originalData">字符串</param>
        /// <returns>加密字符串</returns>
        private static string GetMD5String(string originalData)
        {
            var md5 = System.Security.Cryptography.MD5.Create();
            var buffer = originalData.ToUTF8Bytes();
            var md5Buffer = md5.ComputeHash(buffer);
            md5.Clear();
            var result = from _buffer in md5Buffer select _buffer.ToString("x2");
            return string.Join("", result);
        }

        #endregion

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
                returnCacheKey = GetMD5String(sourceKey);
            }
            else
            {
                if (sourceKey.Length > lengthThreshold)
                {
                    returnCacheKey = GetMD5String(sourceKey);
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