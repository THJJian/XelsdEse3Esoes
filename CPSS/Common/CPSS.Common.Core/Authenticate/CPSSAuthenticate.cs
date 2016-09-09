using System.Web;

namespace CPSS.Common.Core.Authenticate
{
    public class CPSSAuthenticate
    {
        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        /// <returns></returns>
        public static SigninUser GetCurrentUser()
        {
            var user = HttpContext.Current.Items["__Login__User__"] as SigninUser;
            return user;
        }
    }
}