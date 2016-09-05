using CPSS.Data.DataAccess.Interfaces.User.Parameters;
using CPSS.Data.DataAcess.DataModels.User;

namespace CPSS.Data.DataAccess.Interfaces.User
{
    public interface ISiginUserDataAccess
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        SigninUserDataModel QuerySigninUserDataModel(SigninUserParameter parameter);
    }
}