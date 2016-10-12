using CPSS.Data.DataAccess.Interfaces.MenuRight.Parameters;
using CPSS.Data.DataAcess.DataModels.MenuRight;

namespace CPSS.Data.DataAccess.Interfaces.MenuRight
{
    public interface IMenuRightCheckDataAccess
    {
        /// <summary>
        /// 检查是否具有操作及访问权限
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        MenuRightCheckDataModel CheckMenuRightByMenuID(MenuRightCheckParameter parameter);
    }
}