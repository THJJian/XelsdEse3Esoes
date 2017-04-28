using System.Collections.Generic;
using System.Data;
using CPSS.Common.Core.Paging;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters.Client;
using CPSS.Data.DataAcess.DataModels;

namespace CPSS.Data.DataAccess.Interfaces
{
    public partial interface IclientDataAccess
    {
        /// <summary>
        /// 获取往来客户资料列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        PageData<clientDataModel> GetQueryClientList(QueryClientListParameter parameter);

        /// <summary>
        /// 检查往来单位是否已经存在
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        bool CheckClientIsExist(QueryClientListParameter parameter);

        /// <summary>
        /// 根据parentid查询往来客户信息列表(ORDER BY classid DESC)
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        List<clientDataModel> GetClientListByParentID(QueryClientListParameter parameter);

        /// <summary>
        /// 根据classid获取对应行数据
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        clientDataModel GetClientByClassID(QueryClientListParameter parameter);

        /// <summary>
        /// 根据classid更新childnumber字段的值
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        int UpdateChildNumberByClassId(IDbTransaction tran, QueryClientListParameter parameter);

        /// <summary>
        /// 删除往来客户资料(逻辑删除)
        /// </summary>
        /// <returns></returns>
        int Delete(DeleteClientParameter parameter);

        /// <summary>
        /// 恢复删除往来客户资料
        /// </summary>
        /// <returns></returns>
        int ReDelete(DeleteClientParameter parameter);
    }
}