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

        /// <summary>
        /// 将登陆用户保存至在线列表内
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        bool SaveLoginUserToOnline(OnlineSigninUserParameter parameter);

        /// <summary>
        /// 根据UserID_g获取在线用户信息
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        OnlineSigninUserDataModel GetOnlineSigninUserByUserID_g(OnlineSigninUserParameter parameter);

        /// <summary>
        /// 根据UserID获取用户信息
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        SigninUserDataModel FindSininUserDataModelByUserID(OnlineSigninUserParameter parameter);
    }
}