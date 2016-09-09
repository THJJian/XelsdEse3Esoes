// #region << 版 本 注 释 >>
// /*
//  * ========================================================================
//  * 文件名：  UserIPAddressTool.cs 
//  * 创建人：  唐洪建 
//  * 创建时间：2016-09-09 16:08
//  * 版本号：  V1.0.0.0 
//  * 描述：
//  * =====================================================================
//  * 修改人：
//  * 修改时间：2016-09-09 16:08
//  * 版本号： V1.0.0.1
//  * 描述：
// */
// #endregion

using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CPSS.Common.Core
{
    public class UserIPAddressTool
    {
        /// <summary>
        /// 判断是否为真实IP地址
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <returns></returns>
        private static bool IsIPAddress(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        /// 获取用户IP地址
        /// 使用代理服务器或者CDN加速之后需要取特定的Http Header
        /// </summary>
        /// <returns></returns>
        public static string GetRealUserIPAddress()
        {
            //当用户使用代理时，取到的是代理IP
            var result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(result))
            {
                //可能有代理 
                if (result.IndexOf(".", StringComparison.Ordinal) == -1) result = null;
                else
                {
                    if (result.IndexOf(",", StringComparison.Ordinal) != -1)
                    {
                        result = result.Replace(" ", "").Replace("'", "");
                        var temparyip = result.Split(",;".ToCharArray());
                        foreach (
                            var t in
                            temparyip.Where(
                                t =>
                                    IsIPAddress(t) && t.Substring(0, 3) != "10." && t.Substring(0, 7) != "192.168"
                                    && t.Substring(0, 7) != "172.16."))
                        {
                            result = t;
                        }
                        var str = result.Split(',');
                        if (str.Length > 0) result = str[0].Trim();
                    }
                    else if (IsIPAddress(result))
                    {
                        return result;
                    }
                }
            }
            if (string.IsNullOrEmpty(result)) result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(result)) result = HttpContext.Current.Request.UserHostAddress;
            if (string.IsNullOrEmpty(result)) result = "127.0.0.1";
            return result;
        }
    }
}