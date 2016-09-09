using System;
using System.Web;
using System.Web.Security;
using CPSS.Common.Core;
using CPSS.Common.Core.Helper.Config;
using CPSS.Data.DataAccess.Interfaces.User;
using CPSS.Data.DataAccess.Interfaces.User.Parameters;
using CPSS.Service.ViewService.Interfaces.User;
using CPSS.Service.ViewService.ViewModels.User.Request;
using CPSS.Service.ViewService.ViewModels.User.Respond;
using Newtonsoft.Json.Linq;

namespace CPSS.Service.ViewService.User
{
    public class SigninUserViewService : ISigninUserViewService
    {
        private readonly ISiginUserDataAccess mSiginUserDataAccess;

        public SigninUserViewService(ISiginUserDataAccess _siginUserDataAccess)
        {
            this.mSiginUserDataAccess = _siginUserDataAccess;
        }

        /// <summary>
        /// 创建身份票据
        /// </summary>
        /// <param name="user"></param>
        private void CreateFormsAuthentication(SigninUser user)
        {
            DateTime now = DateTime.Now;
            var userData = JObject.FromObject(user).ToString();
            var formsAuthenticationTicket = new FormsAuthenticationTicket(2, user.User_g.ToString(), now, now.Add(FormsAuthentication.Timeout) , false, userData, FormsAuthentication.FormsCookiePath);

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

        public RespondSigninUserViewModel QuerySigninUserViewModel(RequestSigninUserViewModel request)
        {
            var _tmp = request.UserName.Split(':');
            if(_tmp.Length != 2) throw new Exception("登录名格式错误，必须为公司编号:用户名。如->ABC公司:张三");
            var parameter = new SigninUserParameter
            {
                UserName = _tmp[1],
                UserPwd = request.UserPwd,
                CompanySerialNum = _tmp[0]
            };
            var dataModel = this.mSiginUserDataAccess.QuerySigninUserDataModel(parameter);
            if(dataModel == null) throw new Exception("登录名或密码错误。");
            var respond = new RespondSigninUserViewModel
            {
                CurrentUser = new SigninUser
                {
                    CompanySerialNum = dataModel.CompanySerialNum,
                    User_g = Guid.NewGuid(),
                    UserID = dataModel.UserID,
                    UserName = dataModel.UserName,
                    AddressIP = UserIPAddressTool.GetRealUserIPAddress(),
                    ConnectionConfig = new DbConnectionConfig()
                }
            };
            this.CreateFormsAuthentication(respond.CurrentUser);
            return respond;
        }
    }
}