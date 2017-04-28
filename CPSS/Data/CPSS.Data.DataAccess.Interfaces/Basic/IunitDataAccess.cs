using System.Collections.Generic;
using System.Data;
using CPSS.Common.Core.Paging;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters.Unit;
using CPSS.Data.DataAcess.DataModels;

namespace CPSS.Data.DataAccess.Interfaces
{
    public partial interface IunitDataAccess
    {
        /// <summary>
        /// 获取计量单位资料列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        PageData<unitDataModel> GetQueryUnitList(QueryUnitListParameter parameter);

        /// <summary>
        /// 检查计量单位是否已经存在
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        bool CheckUnitIsExist(QueryUnitListParameter parameter);

        /// <summary>
        /// 删除计量单位资料(逻辑删除)
        /// </summary>
        /// <returns></returns>
        int Delete(DeleteUnitParameter parameter);

        /// <summary>
        /// 恢复删除计量单位资料
        /// </summary>
        /// <returns></returns>
        int ReDelete(DeleteUnitParameter parameter);

        /// <summary>
        /// 获取所有没有子节点的计量单位信息
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        List<unitDataModel> GetAllUnit(QueryUnitListParameter parameter);
    }
}