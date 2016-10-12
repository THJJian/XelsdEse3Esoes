using System.Web;
using CPSS.Common.Core.Helper.Extension;
using CPSS.Common.Core.Type;

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
            var user = context.Items[BeforeCompileConstDefined.HttpContext_Login_User] as SigninUser;
            return user;
        }

        /// <summary>
        /// 不使用主数据库连接，默认为true
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool NotUseMainConnection(HttpContext context = null)
        {
            if(context == null) context = HttpContext.Current;
            var _notUseMainConnection = context.Items[BeforeCompileConstDefined.HttpContext_Not_Use_Main_Connection];
            var notUseMainConnection = true;
            if (_notUseMainConnection != null)
                notUseMainConnection = _notUseMainConnection.ToString().ToBool();
            return notUseMainConnection;
        }
    }
}