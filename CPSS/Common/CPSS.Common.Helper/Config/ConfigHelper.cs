using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Xml.Serialization;
using CPSS.Common.Core.Helper.Cached;
using CPSS.Common.Core.Helper.MD5;

namespace CPSS.Common.Core.Helper.Config
{
    /// <summary>
    /// 配置文件帮助类(使用Redis缓存，Redis缓存还未完成)
    /// </summary>
    public class ConfigHelper
    {

        #region Static Fields

        private static readonly SortedDictionary<string, object> LockNames = new SortedDictionary<string, object>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     获取配置文件内容比反序列化为T
        /// </summary>
        /// <param name="configFilePath">配置文件路径</param>
        /// <param name="expiresAt">过期时间(分钟数)</param>
        /// <returns>配置对象</returns>
        public static T GetConfig<T>(string configFilePath, int expiresAt) where T : class
        {
            var cacheKey = string.Concat(configFilePath, typeof(T).FullName, HttpContext.Current == null ? MD5Helper.GetMD5HashCode(AppDomain.CurrentDomain.BaseDirectory) : HttpContext.Current.Request.Url.Host);
            return MemcacheHelper.Get(() =>
            {
                T config;
                lock (GetLockObject(configFilePath))
                {
                    if (!Path.IsPathRooted(configFilePath))
                    {
                        configFilePath = GetMapPath(configFilePath);
                    }
                    var xmlSerializer = new XmlSerializer(typeof(T));
                    using (var streamReader = new StreamReader(configFilePath))
                    {
                        config = xmlSerializer.Deserialize(streamReader) as T;
                    }
                }
                return config;
            }, cacheKey, DateTime.Now.AddMinutes(expiresAt));
        }

        /// <summary>
        ///     获取配置文件内容比反序列化为T(永不过期)
        /// </summary>
        /// <param name="configFilePath">配置文件路径</param>
        /// <returns>配置对象</returns>
        public static T GetConfig<T>(string configFilePath) where T : class
        {
            var cacheKey = string.Concat(configFilePath, typeof(T).FullName, HttpContext.Current == null ? MD5Helper.GetMD5HashCode(AppDomain.CurrentDomain.BaseDirectory) : HttpContext.Current.Request.Url.Host);
            return MemcacheHelper.Get(() =>
            {
                T config;
                lock (GetLockObject(configFilePath))
                {
                    if (!Path.IsPathRooted(configFilePath))
                    {
                        configFilePath = GetMapPath(configFilePath);
                    }
                    try
                    {
                        var xmlSerializer = new XmlSerializer(typeof(T));
                        using (var streamReader = new StreamReader(configFilePath))
                        {
                            config = xmlSerializer.Deserialize(streamReader) as T;
                        }
                    }
                    catch //(InvalidOperationException exception)
                    {
                        return null;
                    }
                }
                return config;
            }, cacheKey);
        }

        #region 不需要通过程序来写配置(配置文件开发过程中手工生成)

        /// <summary>
        ///     保存配置文件
        /// </summary>
        public static void Save<T>(T t, string configFilePath)
        {
            if (!Path.IsPathRooted(configFilePath))
            {
                configFilePath = GetMapPath(configFilePath);
            }
            var dirPath = Path.GetDirectoryName(configFilePath);
            if (dirPath != null && !Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            if (!File.Exists(configFilePath))
            {
                File.Create(configFilePath).Close();
            }
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var writer = new StreamWriter(configFilePath, false))
            {
                xmlSerializer.Serialize(writer, t);
            }
        }

        #endregion

        #endregion

        #region Methods

        private static object GetLockObject(string name)
        {
            object result;
            LockNames.TryGetValue(name, out result);
            if (result != null)
            {
                return result;
            }
            result = new object();
            lock (LockNames)
            {
                if (!LockNames.ContainsKey(name))
                {
                    LockNames.Add(name, result);
                }
            }
            return result;
        }

        private static string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            if (strPath.StartsWith("~"))
            {
                strPath = strPath.Substring(1);
            }
            strPath = strPath.Replace("/", "\\");

            if (strPath.StartsWith("\\"))
            {
                strPath = strPath.Substring(1);
            }
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
        }

        #endregion
    }
}