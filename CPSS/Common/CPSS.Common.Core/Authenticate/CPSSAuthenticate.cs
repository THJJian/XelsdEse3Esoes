using System.Web;

namespace CPSS.Common.Core.Authenticate
{
    public class CPSSAuthenticate
    {
        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        /// <returns></returns>
        public static SigninUser GetCurrentUser(HttpContext context = null)
        {
            if(context == null) context = HttpContext.Current;
            var user = context.Items["__Login__User__"] as SigninUser;
            return user;
        }
    }
}