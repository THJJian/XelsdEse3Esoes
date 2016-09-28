using System.Collections.Generic;
using CPSS.Data.DataAccess.Interfaces.MainPage.Parameters;
using CPSS.Data.DataAcess.DataModels.MainPage;

namespace CPSS.Data.DataAccess.Interfaces.MainPage
{
    public interface ILeftNavMenuDataAccess
    {
        /// <summary>
        /// 获取用户菜单列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        List<LeftNavMenuDataModel> GetLeftNavMenuDataModels(LeftNavMenuParameter parameter);
    }
}