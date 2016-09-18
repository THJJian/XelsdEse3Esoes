using System.Security.Cryptography;
using System.Text;
using CPSS.Common.Core.Helper.Extension;

namespace CPSS.Common.Core.Helper.MD5
{
    public class MD5Helper
    {

        /// <summary>
        /// 获取MD5码
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string GetMD5HashCode(byte[] buffer)
        {
            var md5Provider = new MD5CryptoServiceProvider();
            var hashCode = md5Provider.ComputeHash(buffer);
            var builder = new StringBuilder();
            foreach (var code in hashCode)
            {
                builder.Append(code.ToString("x2"));
            }
            return builder.ToString();
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