using System;
using System.Web;
using CPSS.Common.Core;
using CPSS.Common.Core.Authenticate;
using CPSS.Common.Core.Exception;
using CPSS.Common.Core.Helper.Cached;
using CPSS.Common.Core.Helper.Extension;
using CPSS.Common.Core.Helper.MD5;
using CPSS.Common.Core.Helper.WebConfig;
using CPSS.Common.Core.Type;
using CPSS.Data.DataAccess.Interfaces.MongoDB;
using CPSS.Data.DataAccess.Interfaces.User;
using CPSS.Data.DataAccess.Interfaces.User.Parameters;
using CPSS.Service.ViewService.Interfaces.User;
using CPSS.Service.ViewService.ViewModels.MongoDb.Request;
using CPSS.Service.ViewService.ViewModels.User.Request;
using CPSS.Service.ViewService.ViewModels.User.Respond;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CPSS.Service.ViewService.User
{
    public class SigninUserViewService : ISigninUserViewService
    {
        private const string preCacheKey = "CPSS.Service.ViewService.User.SigninUserViewService.{0}";
        private readonly ISiginUserDataAccess mSiginUserDataAccess;
        private readonly ICompanyInfoViewService mCompanyInfoViewService;
        private readonly IMongoDbDataAccess mMongoDbDataAccess;

        public SigninUserViewService(ISiginUserDataAccess _siginUserDataAccess, ICompanyInfoViewService _companyInfoViewService, IMongoDbDataAccess _mongoDbDataAccess)
        {
            this.mSiginUserDataAccess = _siginUserDataAccess;
            this.mCompanyInfoViewService = _companyInfoViewService;
            this.mMongoDbDataAccess = _mongoDbDataAccess;
        }

        public RespondWebViewData<RespondSigninUserViewModel> QuerySigninUserViewModel(RequestSigninUserViewModel request)
        {
            var userID_g = Guid.NewGuid();
            return MemcacheHelper.Get(() =>
            {
                var _tmp = request.UserName.Split(':');
                if (_tmp.Length != 2) return  new RespondWebViewData<RespondSigninUserViewModel>(WebViewErrorCode.SigninInfoError);
                var parameter = new SigninUserParameter
                {
                    UserName = _tmp[1],
                    UserPwd = MD5Helper.GetMD5HashCode(request.UserPwd),
                    CompanySerialNum = _tmp[0]
                };
                var dataModel = this.mSiginUserDataAccess.QuerySigninUserDataModel(parameter);
                if (dataModel == null) return new RespondWebViewData<RespondSigninUserViewModel>(WebViewErrorCode.UserNameOrPwdError);
                //var companyInfoRequest = new RequestCompanyInfoViewModel
                //{
                //    CompanyID = dataModel.CompanySerialNum
                //};
                //var companyInfo = this.mCompanyInfoViewService.GetCompanyInfoViewModel(companyInfoRequest);
                //var connectionConfig = new DbConnectionConfig
                //{
                //    ConnectTimeout = companyInfo.ConnectTimeout,
                //    Database = companyInfo.Database,
                //    Password = companyInfo.Password,
                //    Server = companyInfo.Server,
                //    UserID = companyInfo.UserID
                //};
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
                            IsSysManager = dataModel.Manager && dataModel.isSystem
                        }
                    }
                };
                this.SaveLoginUserToOnline(new RequestSigninUserViewModel
                {
                    UserID = dataModel.UserID,
                    UserName = dataModel.UserName,
                    UserID_g= userID_g
                });
                FormsAuthenticationTicketManage.CreateFormsAuthentication(userID_g);
                HttpContext.Current.Items.Add(BeforeCompileConstDefined.HttpContext_Login_User, _respond.Data.CurrentUser);
                var _mongo_db_request = new RequestMongoDbViewModel
                {
                    LogName = "登录操作",
                    LogData =  JObject.FromObject(_respond).ToString(Formatting.None),
                    LogTime = DateTime.Now
                };
                this.mMongoDbDataAccess.Save(_mongo_db_request);
                return _respond;
            }, userID_g.ToString());
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
            return MemcacheHelper.Get(() =>
            {
                var parameter = new OnlineSigninUserParameter
                {
                    SGuid = request.SGuid,
                    UserIP = request.AddressIP
                };
                var dataModel = this.mSiginUserDataAccess.GetOnlineSigninUserByUserID_g(parameter);
                if (dataModel == null)
                    return new RespondWebViewData<RespondOnlineSigninUserViewModel>(WebViewErrorCode.LoginRequired);
                return new RespondWebViewData<RespondOnlineSigninUserViewModel>
                {
                    Data = new RespondOnlineSigninUserViewModel
                    {
                        UserID = dataModel.UserID,
                        LoginName = dataModel.LoginName,
                        SGuid = dataModel.SGuid,
                        ExpTime = dataModel.ExpTime,
                        UserIP = dataModel.UserIP
                    }
                };
            }, string.Format(preCacheKey, "GetOnlineSigninUserByUserID_g"), 
            DateTime.Now.AddMinutes(WebConfigHelper.MemCachedExpTime()), 
            request.SGuid, 
            request.AddressIP);
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
            HttpContext.Current.Items.Add(BeforeCompileConstDefined.HttpContext_Login_User, respond.Data.CurrentUser);
            return respond;
        }
    }
}