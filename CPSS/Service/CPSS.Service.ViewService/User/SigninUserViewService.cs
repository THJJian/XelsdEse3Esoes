using System;
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

        public SigninUserViewService(ISiginUserDataAccess _siginUserDataAccess)
        {
            this.mSiginUserDataAccess = _siginUserDataAccess;
        }

        private void Create

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
                UserID = dataModel.UserID,
                CompanyName = dataModel.CompanyName,
                UserID_g = Guid.NewGuid(),
                UserName = dataModel.UserName
            };

            return respond;
        }
    }
}