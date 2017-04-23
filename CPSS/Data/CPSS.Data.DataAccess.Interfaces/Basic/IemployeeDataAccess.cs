using System.Collections.Generic;
using System.Data;
using CPSS.Common.Core.Paging;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters.Employee;
using CPSS.Data.DataAcess.DataModels;

namespace CPSS.Data.DataAccess.Interfaces
{
    public partial interface IemployeeDataAccess
    {
        /// <summary>
        /// 获取职员资料列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        PageData<employeeDataModel> GetQueryEmployeeList(QueryEmployeeListParameter parameter);

        /// <summary>
        /// 根据parentid查询职员信息列表(ORDER BY classid DESC)
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        List<employeeDataModel> GetEmployeeListByParentID(QueryEmployeeListParameter parameter);

        /// <summary>
        /// 根据classid获取对应行数据
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        employeeDataModel GetEmployeeByClassID(QueryEmployeeListParameter parameter);

        /// <summary>
        /// 根据classid更新childnumber字段的值
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        int UpdateChildNumberByClassId(IDbTransaction tran, QueryEmployeeListParameter parameter);

        /// <summary>
        /// 删除职员资料(逻辑删除)
        /// </summary>
        /// <returns></returns>
        int Delete(DeleteEmployeeParameter parameter);

        /// <summary>
        /// 恢复删除职员资料
        /// </summary>
        /// <returns></returns>
        int ReDelete(DeleteEmployeeParameter parameter);
    }
}