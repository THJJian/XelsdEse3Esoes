using CPSS.Common.Core.Paging;
using CPSS.Data.DataAccess.Interfaces.SystemManage.UserManage.Parameters;
using CPSS.Data.DataAcess.DataModels.SystemManage.UserManage;

namespace CPSS.Data.DataAccess.Interfaces.SystemManage.UserManage
{
    public interface IUserDataAccess
    {
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        PageData<UserDataModel> GetUserPageData(QueryUserParameter parameter);

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int AddUser(UserDataModel data);

        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int UpdateUser(UserDataModel data);

        /// <summary>
        /// 更改用户状态
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int ChangeStatus(UserDataModel data);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int DeletedUser(UserDataModel data);
    }
}