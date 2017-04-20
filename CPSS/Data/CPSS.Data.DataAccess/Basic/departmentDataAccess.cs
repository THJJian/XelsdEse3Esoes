using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CPSS.Common.Core.Paging;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters;
using CPSS.Data.DataAcess.DataModels;

namespace CPSS.Data.DataAccess
{
    public partial class departmentDataAccess
    {
        public PageData<departmentDataModel> GetQueryDepartmentList(QueryDepartmentListParameter parameter)
        {
            var isSearch = !(string.IsNullOrEmpty(parameter.SerialNumber) && string.IsNullOrEmpty(parameter.Name) && parameter.Status == 0 && parameter.Deleted == 0);

            this.ExecuteSQL = string.Format("SELECT * FROM dbo.department WHERE {0} serialnumber LIKE '%{1}%' AND name LIKE '%{2}%' AND [status] IN({3}) AND deleted IN({4})",
                isSearch ? string.Empty : string.Format("parentid='{0}' AND", parameter.ParentId),
                parameter.SerialNumber,
                parameter.Name,
                parameter.Status == 0 ? "1, 2" : parameter.Status.ToString(),
                parameter.Deleted == 0 ? "1, 2":parameter.Deleted.ToString()
                );
            return this.ExecuteReadSqlTodepartmentDataModelPageData("depid", parameter.PageIndex, parameter.PageSize, "classid ASC, [sort] DESC");
        }

        public List<departmentDataModel> GetDepartmentListByParentID(QueryDepartmentListParameter parameter)
        {
            this.ExecuteSQL = "SELECT * FROM dbo.department WHERE parentid=@parentid ORDER BY classid DESC";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@parentid", SqlDbType.VarChar){ Value = parameter.ParentId}
            };
            return this.ExecuteReadSqlTodepartmentDataModelList();
        }

        public departmentDataModel GetDepartmentByClassID(QueryDepartmentListParameter parameter)
        {
            this.ExecuteSQL = "SELECT * FROM dbo.department WHERE classid=@classid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@classid", SqlDbType.VarChar){ Value = parameter.ParentId}
            };
            return this.ExecuteReadSqlTodepartmentDataModel();
        }

        public int UpdateChildNumberByClassId(IDbTransaction tran, QueryDepartmentListParameter parameter)
        {
            this.ExecuteSQL = "UPDATE dbo.department SET childnumber=childnumber+1 WHERE classid=@classid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@classid", SqlDbType.VarChar){ Value = parameter.ParentId}
            };
            return this.ExecuteNonQuery(tran);
        }

        public int Delete(DeleteDepartmentParameter parameter)
        {
            this.ExecuteSQL = "UPDATE dbo.department SET deleted=1 WHERE depid=@depid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@subcomid", parameter.depid)
            };
            return this.ExecuteNonQuery();
        }

        public int ReDelete(DeleteDepartmentParameter parameter)
        {
            this.ExecuteSQL = "UPDATE dbo.department SET deleted=0 WHERE depid=@depid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@subcomid", parameter.depid)
            };
            return this.ExecuteNonQuery();
        }
    }
}