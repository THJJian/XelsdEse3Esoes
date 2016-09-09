using System;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json.Linq;

namespace CPSS.Common.Core.Authenticate
{
    public class RestoreUserIdentity
    {
        /// <summary>
        /// 恢复用户身份
        /// </summary>
        /// <param name="context"></param>
        public static void RestoreUserByCookie(HttpContext context)
        {
            var user = context.Items["__Login__User__"] as SigninUser;
            if (user != null) return;

            HttpCookie userCookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (userCookie == null || string.IsNullOrEmpty(userCookie.Value)) return;
            try
            {
                var authenticationTicket = FormsAuthentication.Decrypt(userCookie.Value);
                if (authenticationTicket == null || authenticationTicket.Expired) return;
                var userData = authenticationTicket.UserData;
                user = JObject.Parse(userData).ToObject<SigninUser>();
                if (user.AddressIP.Equals(UserIPAddressTool.GetRealUserIPAddress()))
                {

                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

    }
}