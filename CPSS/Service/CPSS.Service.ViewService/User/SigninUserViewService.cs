using System;
using System.Web;
using CPSS.Common.Core;
using CPSS.Common.Core.Authenticate;
using CPSS.Common.Core.Helper.Cached;
using CPSS.Common.Core.Helper.Extension;
using CPSS.Data.DataAccess.Interfaces.User;
using CPSS.Data.DataAccess.Interfaces.User.Parameters;
using CPSS.Service.ViewService.Interfaces.User;
using CPSS.Service.ViewService.ViewModels.User.Request;
using CPSS.Service.ViewService.ViewModels.User.Respond;

namespace CPSS.Service.ViewService.User
{
    public class SigninUserViewService : ISigninUserViewService
    {
        private readonly ISiginUserDataAccess mSiginUserDataAccess;
        private readonly ICompanyInfoViewService mCompanyInfoViewService;

        public SigninUserViewService(ISiginUserDataAccess _siginUserDataAccess, ICompanyInfoViewService _companyInfoViewService)
        {
            this.mSiginUserDataAccess = _siginUserDataAccess;
            this.mCompanyInfoViewService = _companyInfoViewService;
        }

        public RespondSigninUserViewModel QuerySigninUserViewModel(RequestSigninUserViewModel request)
        {
            var userID_g = Guid.NewGuid();
            var respond = MemcacheHelper.Get(() =>
            {
                var _tmp = request.UserName.Split(':');
                if (_tmp.Length != 2) throw new Exception("登录名格式错误，必须为公司编号:用户名。如->ABC公司:张三");
                var parameter = new SigninUserParameter
                {
                    UserName = _tmp[1],
                    UserPwd = request.UserPwd,
                    CompanySerialNum = _tmp[0]
                };
                var dataModel = this.mSiginUserDataAccess.QuerySigninUserDataModel(parameter);
                if (dataModel == null) throw new Exception("登录名或密码错误。");
                var companyInfoRequest = new RequestCompanyInfoViewModel
                {
                    CompanyID = dataModel.CompanySerialNum.ToInt32()
                };
                var companyInfo = this.mCompanyInfoViewService.GetCompanyInfoViewModel(companyInfoRequest);
                var connectionConfig = new DbConnectionConfig
                {
                    ConnectTimeout = companyInfo.ConnectTimeout,
                    Database = companyInfo.Database,
                    Password = companyInfo.Password,
                    Server = companyInfo.Server,
                    UserID = companyInfo.UserID
                };
                var _respond = new RespondSigninUserViewModel
                {
                    CurrentUser = new SigninUser
                    {
                        CompanySerialNum = dataModel.CompanySerialNum,
                        UserID_g = userID_g,
                        UserID = dataModel.UserID,
                        UserName = dataModel.UserName,
                        AddressIP = UserIPAddressTool.GetRealUserIPAddress(),
                        ConnectionConfig = connectionConfig
                    }
                };
                return _respond;
            }, userID_g.ToString());

            var context = HttpContext.Current;
            var now = DateTime.Now;
            var expTime = now.ToShortDateString().ToDateTime().AddDays(1).AddSeconds(-1);//当日23:59:59
            var _parameter = new OnlineSigninUserParameter
            {
                  UserID = respond.CurrentUser.UserID,
                  Browser = context.Request.Browser.Browser,
                  ExpTime = expTime,
                  LoginName = respond.CurrentUser.UserName,
                  LoginTime = now,
                  OverTime = expTime,
                  SGuid = userID_g,
                  UserIP = UserIPAddressTool.GetRealUserIPAddress()
            };
            this.mSiginUserDataAccess.SaveLoginUserToOnline(_parameter);
            FormsAuthenticationTicketManage.CreateFormsAuthentication(userID_g);
            HttpContext.Current.Items.Add("__Login__User__", respond.CurrentUser);
            return respond;
        }
    }
}