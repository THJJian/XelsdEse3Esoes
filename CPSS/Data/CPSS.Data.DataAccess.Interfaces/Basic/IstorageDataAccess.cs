using System.Collections.Generic;
using System.Data;
using CPSS.Common.Core.Paging;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters.Storage;
using CPSS.Data.DataAcess.DataModels;

namespace CPSS.Data.DataAccess.Interfaces
{
    public partial interface IstorageDataAccess
    {
        /// <summary>
        /// 获取仓库资料列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        PageData<storageDataModel> GetQueryStorageList(QueryStorageListParameter parameter);

        /// <summary>
        /// 根据parentid查询仓库信息列表(ORDER BY classid DESC)
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        List<storageDataModel> GetStorageListByParentID(QueryStorageListParameter parameter);

        /// <summary>
        /// 根据classid获取对应行数据
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        storageDataModel GetStorageByClassID(QueryStorageListParameter parameter);

        /// <summary>
        /// 根据classid更新childnumber字段的值
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        int UpdateChildNumberByClassId(IDbTransaction tran, QueryStorageListParameter parameter);

        /// <summary>
        /// 删除仓库资料(逻辑删除)
        /// </summary>
        /// <returns></returns>
        int Delete(DeleteStorageParameter parameter);

        /// <summary>
        /// 恢复删除仓库资料
        /// </summary>
        /// <returns></returns>
        int ReDelete(DeleteStorageParameter parameter);
    }
}