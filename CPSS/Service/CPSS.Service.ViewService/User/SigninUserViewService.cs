using System;
using System.Web;
using CPSS.Common.Core;
using CPSS.Common.Core.Authenticate;
using CPSS.Common.Core.Exception;
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

        public RespondWebViewData<RespondSigninUserViewModel> QuerySigninUserViewModel(RequestSigninUserViewModel request)
        {
            var userID_g = Guid.NewGuid();
            var respond = MemcacheHelper.Get(() =>
            {
                var _tmp = request.UserName.Split(':');
                if (_tmp.Length != 2) return  new RespondWebViewData<RespondSigninUserViewModel>(WebViewErrorCode.SigninInfoError);
                var parameter = new SigninUserParameter
                {
                    UserName = _tmp[1],
                    UserPwd = request.UserPwd,
                    CompanySerialNum = _tmp[0]
                };
                var dataModel = this.mSiginUserDataAccess.QuerySigninUserDataModel(parameter);
                if (dataModel == null) return new RespondWebViewData<RespondSigninUserViewModel>(WebViewErrorCode.UserNameOrPwdError);
                var companyInfoRequest = new RequestCompanyInfoViewModel
                {
                    CompanyID = dataModel.CompanySerialNum
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
                var _respond = new RespondWebViewData<RespondSigninUserViewModel>
                {
                    Data = new RespondSigninUserViewModel
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
                    }
                };
                return _respond;
            }, userID_g.ToString());
            this.SaveLoginUserToOnline(new RequestSigninUserViewModel
            {
                UserID = respond.Data.CurrentUser.UserID,
                UserName = respond.Data.CurrentUser.UserName,
                UserID_g= respond.Data.CurrentUser.UserID_g
            });
            FormsAuthenticationTicketManage.CreateFormsAuthentication(userID_g);
            HttpContext.Current.Items.Add("__Login__User__", respond.Data.CurrentUser);
            return respond;
        }

        /// <summary>
        /// 将登陆用户保存至在线列表内
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool SaveLoginUserToOnline(RequestSigninUserViewModel request)
        {
            var context = HttpContext.Current;
            var now = DateTime.Now;
            var expTime = now.ToShortDateString().ToDateTime().AddDays(1).AddSeconds(-1);//当日23:59:59
            var _parameter = new OnlineSigninUserParameter
            {
                UserID = request.UserID,
                Browser = context.Request.Browser.Browser,
                ExpTime = expTime,
                LoginName = request.UserName,
                LoginTime = now,
                OverTime = expTime,
                SGuid = request.UserID_g,
                UserIP = UserIPAddressTool.GetRealUserIPAddress()
            };
            return this.mSiginUserDataAccess.SaveLoginUserToOnline(_parameter);
        }

        public RespondWebViewData<RespondOnlineSigninUserViewModel> GetOnlineSigninUserByUserID_g(RequestOnlineSigninUserViewModel request)
        {
            var parameter = new OnlineSigninUserParameter
            {
                SGuid = request.SGuid
            };
            var dataModel = this.mSiginUserDataAccess.GetOnlineSigninUserByUserID_g(parameter);
            if(dataModel == null) return new RespondWebViewData<RespondOnlineSigninUserViewModel>(WebViewErrorCode.LoginRequired);
            return new RespondWebViewData<RespondOnlineSigninUserViewModel>
            {
                Data =  new RespondOnlineSigninUserViewModel
                {
                    UserID = dataModel.UserID,
                    LoginName = dataModel.LoginName,
                    SGuid = dataModel.SGuid,
                    ExpTime = dataModel.ExpTime,
                    UserIP = dataModel.UserIP
                }
            };
        }

        public RespondWebViewData<RespondSigninUserViewModel> FindSininUserDataModelByUserID(RequestOnlineSigninUserViewModel request)
        {
            var parameter = new OnlineSigninUserParameter
            {
                UserID = request.UserID
            };
            var dataModel = this.mSiginUserDataAccess.FindSininUserDataModelByUserID(parameter);
            if(dataModel==null) return new RespondWebViewData<RespondSigninUserViewModel>(WebViewErrorCode.NotExistUserInfo);
            var companyInfoRequest = new RequestCompanyInfoViewModel
            {
                CompanyID = dataModel.CompanySerialNum
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
            FormsAuthenticationTicketManage.RenewTicketIfOld(request.SGuid);
            var respond = new RespondWebViewData<RespondSigninUserViewModel>
            {
                Data = new RespondSigninUserViewModel
                {
                    CurrentUser = new SigninUser
                    {
                        CompanySerialNum = dataModel.CompanySerialNum,
                        UserID_g = request.SGuid,
                        UserID = dataModel.UserID,
                        UserName = dataModel.UserName,
                        AddressIP = UserIPAddressTool.GetRealUserIPAddress(),
                        ConnectionConfig = connectionConfig
                    }
                }
            };
            HttpContext.Current.Items.Add("__Login__User__", respond.Data.CurrentUser);
            return respond;
        }
    }
}