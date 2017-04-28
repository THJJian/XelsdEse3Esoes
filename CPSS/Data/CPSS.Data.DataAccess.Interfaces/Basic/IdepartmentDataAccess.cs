using System.Collections.Generic;
using System.Data;
using CPSS.Common.Core.Paging;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters.Department;
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
        /// 检查部门是否已经存在
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        bool CheckDepartmentIsExist(QueryDepartmentListParameter parameter);

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

        /// <summary>
        /// 获取所有没有子节点的部门信息
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        List<departmentDataModel> GetAllDepartment(QueryDepartmentListParameter parameter);
    }
}