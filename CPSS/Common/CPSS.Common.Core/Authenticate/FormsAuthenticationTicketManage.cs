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

        /// <summary>
        /// 条件地更新 System.Web.Security.FormsAuthenticationTicket 的发出日期和时间以及过期日期和时间。
        /// </summary>
        /// <param name="userID_g"></param>
        public static void RenewTicketIfOld(Guid userID_g)
        {
            HttpCookie userCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (userCookie != null && string.IsNullOrEmpty(userCookie.Value)) CreateFormsAuthentication(userID_g);
            var authenticationTicket = FormsAuthentication.Decrypt(userCookie.Value);
            FormsAuthentication.RenewTicketIfOld(authenticationTicket);
        }
    }
}