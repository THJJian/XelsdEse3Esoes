using System;
using System.Text;

namespace CPSS.Common.Core.Helper.Extension
{

    /// <summary>
    /// .net基础类型扩展
    /// </summary>
    public static class BasicTypeExtension
    {

        #region String Converter Object

        /// <summary>
        /// UTF8编码格式的字符串转换成byte[]
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static byte[] ToUTF8Bytes(this string source)
        {
            var bytes = Encoding.UTF8.GetBytes(source);
            return bytes;
        }

        /// <summary>
        /// ASCII编码格式的字符串转换成byte[]
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static byte[] ToASCIIBytes(this string source)
        {
            var bytes = Encoding.ASCII.GetBytes(source);
            return bytes;
        }

        /// <summary>
        /// 环境默认编码格式的字符串转换成byte[]
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string source)
        {
            var bytes = Encoding.Default.GetBytes(source);
            return bytes;
        }

        /// <summary>
        /// 将字符串转换成Int16数据类型
        /// </summary>
        /// <param name="source"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public static short ToInt16(this string source, short defaultVal = 0)
        {
            if (string.IsNullOrEmpty(source)) return defaultVal;
            short result;
            return Int16.TryParse(source, out result) ? result : defaultVal;
        }

        /// <summary>
        /// 将字符串转换成Int32数据类型
        /// </summary>
        /// <param name="source"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public static int ToInt32(this string source, int defaultVal = 0)
        {
            if (string.IsNullOrEmpty(source)) return defaultVal;
            int result;
            return Int32.TryParse(source, out result) ? result : defaultVal;
        }

        /// <summary>
        /// 将字符串转换成Int64数据类型
        /// </summary>
        /// <param name="source"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public static long ToInt64(this string source, long defaultVal = 0)
        {
            if (string.IsNullOrEmpty(source)) return defaultVal;
            long result;
            return Int64.TryParse(source, out result) ? result : defaultVal;
        }

        /// <summary>
        /// 将一个日期格式的字符串转换成日期
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string source)
        {
            DateTime outVal;
            var defaultVal = DateTime.Now;
            if (DateTime.TryParse(source, out outVal)) defaultVal = outVal;
            return defaultVal;
        }

        /// <summary>
        /// 讲一个GUID格式的字符串转换成GUID
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Guid ToGuid(this string source)
        {
            Guid outVal;
            var defaultVal = Guid.Empty;
            if (Guid.TryParse(source, out outVal)) defaultVal = outVal;
            return defaultVal;
        }

        #endregion

        #region Byte Converter Object

        /// <summary>
        /// 将byte[]转换为UTF8编码格式的字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToUTF8String(this byte[] source)
        {
            var result = Encoding.UTF8.GetString(source);
            return result;
        }

        /// <summary>
        /// 将byte[]转换为ASCII编码格式的字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToASCIIString(this byte[] source)
        {
            var result = Encoding.ASCII.GetString(source);
            return result;
        }

        /// <summary>
        /// 将byte[]转换为环境默认编码格式的字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToString(this byte[] source)
        {
            var result = Encoding.Default.GetString(source);
            return result;
        }

        #endregion

        #region Bool Converter Object

        /// <summary>
        /// 将bool值转换为其等效字符串表示形式（小写“true”或“false”）。
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToLowerString(this bool source)
        {
            var result = source.ToString().ToLower();
            return result;
        }

        #endregion

    }
}