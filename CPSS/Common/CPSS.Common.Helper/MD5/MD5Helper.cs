using System.Linq;
using System.Security.Cryptography;
using CPSS.Common.Core.Helper.Extension;

namespace CPSS.Common.Core.Helper.MD5
{
    public class MD5Helper
    {

        /// <summary>
        /// 获取MD5码
        /// </summary>
        /// <param name="buffers"></param>
        /// <returns></returns>
        public static string GetMD5HashCode(byte[] buffers)
        {
            var md5Provider = new MD5CryptoServiceProvider();
            var md5Buffers = md5Provider.ComputeHash(buffers);
            var result = from buffer in md5Buffers select buffer.ToString("x2");
            return string.Join("", result);
        }

        /// <summary>
        /// 获取MD5码
        /// </summary>
        /// <param name="originalData"></param>
        /// <returns></returns>
        public static string GetMD5HashCode(string originalData)
        {
            var buffer = originalData.ToUTF8Bytes();
            return GetMD5HashCode(buffer);
        } 
    }
}