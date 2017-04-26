using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CPSS.Common.Core.Paging;
using CPSS.Common.Core.Type.EnumType;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters.Department;
using CPSS.Data.DataAcess.DataModels;

namespace CPSS.Data.DataAccess
{
    public partial class departmentDataAccess
    {
        public PageData<departmentDataModel> GetQueryDepartmentList(QueryDepartmentListParameter parameter)
        {
            var isSearch = !(string.IsNullOrEmpty(parameter.SerialNumber) && string.IsNullOrEmpty(parameter.Name) && parameter.Status == 0 && parameter.Deleted == 0);

            this.ExecuteSQL = string.Format("SELECT * FROM dbo.department WHERE {0} serialnumber LIKE '%{1}%' AND name LIKE '%{2}%' AND pinyin LIKE '%{5}%' AND [status] IN({3}) AND deleted IN({4})",
                isSearch ? string.Empty : string.Format("parentid='{0}' AND", parameter.ParentId),
                parameter.SerialNumber,
                parameter.Name,
                parameter.Status == (short)CommonStatus.Default ? string.Concat((short)CommonStatus.Used, ",", (short)CommonStatus.Stopped) : parameter.Status.ToString(),
                parameter.Deleted == (short)CommonDeleted.Default ? string.Concat((short)CommonDeleted.Deleted, ",", (short)CommonDeleted.NotDeleted) : parameter.Deleted.ToString(),
                parameter.Spelling
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
            this.ExecuteSQL = "UPDATE dbo.department SET deleted=@deleted WHERE depid=@depid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@deleted", parameter.Deleted), 
                new SqlParameter("@depid", parameter.depid)
            };
            return this.ExecuteNonQuery();
        }

        public int ReDelete(DeleteDepartmentParameter parameter)
        {
            this.ExecuteSQL = "UPDATE dbo.department SET deleted=@deleted WHERE depid=@depid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@deleted", parameter.Deleted), 
                new SqlParameter("@depid", parameter.depid)
            };
            return this.ExecuteNonQuery();
        }

        public List<departmentDataModel> GetAllDepartment(QueryDepartmentListParameter parameter)
        {
            this.ExecuteSQL = string.Format("SELECT * FROM dbo.department WHERE childnumber=0 AND (serialnumber LIKE '%{0}%' OR name LIKE '%{0}%' OR pinyin LIKE '%{0}%' OR comment LIKE '%{0}%') AND ISNULL(parentid,'')<>'' AND [status]={1} AND deleted={2}",
                parameter.SerialNumber,
                (short)CommonStatus.Used,
                (short)CommonDeleted.NotDeleted);
            return this.ExecuteReadSqlTodepartmentDataModelList();
        }
    }
}