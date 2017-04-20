using System.Collections.Generic;
using System.Data;
using CPSS.Common.Core.Paging;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters;
using CPSS.Data.DataAcess.DataModels;

namespace CPSS.Data.DataAccess.Interfaces
{
    public partial interface IdepartmentDataAccess
    {
        /// <summary>
        /// 获取部门资料列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        PageData<departmentDataModel> GetQueryDepartmentList(QueryDepartmentListParameter parameter);

        /// <summary>
        /// 根据parentid查询部门信息列表(ORDER BY classid DESC)
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        List<departmentDataModel> GetDepartmentListByParentID(QueryDepartmentListParameter parameter);

        /// <summary>
        /// 根据classid获取对应行数据
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        departmentDataModel GetDepartmentByClassID(QueryDepartmentListParameter parameter);

        /// <summary>
        /// 根据classid更新childnumber字段的值
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        int UpdateChildNumberByClassId(IDbTransaction tran, QueryDepartmentListParameter parameter);

        /// <summary>
        /// 删除部门资料(逻辑删除)
        /// </summary>
        /// <returns></returns>
        int Delete(DeleteDepartmentParameter parameter);

        /// <summary>
        /// 恢复删除部门资料
        /// </summary>
        /// <returns></returns>
        int ReDelete(DeleteDepartmentParameter parameter);
    }
}