using System.Collections.Generic;
using System.Data;
using CPSS.Common.Core.Paging;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters.SubCompany;
using CPSS.Data.DataAcess.DataModels;

namespace CPSS.Data.DataAccess.Interfaces
{
    public partial interface IsubcompanyDataAccess
    {
        /// <summary>
        /// 获取分公司资料列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        PageData<subcompanyDataModel> GetQuerySubCompanyList(QuerySubCompanyListParameter parameter);

        /// <summary>
        /// 根据parentid查询分公司信息列表(ORDER BY classid DESC)
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        List<subcompanyDataModel> GetSubCompanyListByParentID(QuerySubCompanyListParameter parameter);

        /// <summary>
        /// 根据classid获取对应行数据
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        subcompanyDataModel GetSubCompanyByClassID(QuerySubCompanyListParameter parameter);

        /// <summary>
        /// 根据classid更新childnumber字段的值
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        int UpdateChildNumberByClassId(IDbTransaction tran, QuerySubCompanyListParameter parameter);

        /// <summary>
        /// 删除子公司资料(逻辑删除)
        /// </summary>
        /// <returns></returns>
        int Delete(DeleteSubCompanyParameter parameter);

        /// <summary>
        /// 恢复删除子公司资料
        /// </summary>
        /// <returns></returns>
        int ReDelete(DeleteSubCompanyParameter parameter);
    }
}