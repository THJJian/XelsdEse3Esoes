using System;
using System.Web;
using System.Web.Security;

namespace CPSS.Common.Core.Authenticate
{
    public class FormsAuthenticationTicketManage
    {
        /// <summary>
        /// 创建身份票据
        /// </summary>
        /// <param name="userID_g"></param>
        public static void CreateFormsAuthentication(Guid userID_g)
        {
            DateTime now = DateTime.Now;
            var formsAuthenticationTicket = new FormsAuthenticationTicket(2, "UserID_g", now, now.Add(FormsAuthentication.Timeout), false, userID_g.ToString(), FormsAuthentication.FormsCookiePath);

            var value = FormsAuthentication.Encrypt(formsAuthenticationTicket);
            var httpCookie = new HttpCookie(FormsAuthentication.FormsCookieName, value)
            {
                Expires = formsAuthenticationTicket.Expiration,
                Path = formsAuthenticationTicket.CookiePath,
                HttpOnly = true,
                Secure = false,
                Domain = FormsAuthentication.CookieDomain
            };
            HttpContext.Current.Response.Cookies.Add(httpCookie);
        }

    }
}