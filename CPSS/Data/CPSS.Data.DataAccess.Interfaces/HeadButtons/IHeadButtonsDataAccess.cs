using System.Collections.Generic;
using CPSS.Data.DataAccess.Interfaces.HeadButtons.Parameters;
using CPSS.Data.DataAcess.DataModels.HeadButtons;

namespace CPSS.Data.DataAccess.Interfaces.HeadButtons
{
    public interface IHeadButtonsDataAccess
    {
        /// <summary>
        /// 根据菜单ID获取有权限的操作按钮
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        IList<HeadButtonsDataModel> QueryHeadButtonsViewModelsByMenuID(HeadButtonsParameter parameter);

    }
}