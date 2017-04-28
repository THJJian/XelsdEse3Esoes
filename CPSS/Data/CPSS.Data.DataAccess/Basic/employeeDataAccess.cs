using System.Collections.Generic;
using System.Data;
using CPSS.Common.Core.Paging;
using CPSS.Common.Core.Type.EnumType;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters.Employee;
using CPSS.Data.DataAcess.DataModels;
using System.Data.SqlClient;

namespace CPSS.Data.DataAccess
{
    public partial class employeeDataAccess
    {
        public PageData<employeeDataModel> GetQueryEmployeeList(QueryEmployeeListParameter parameter)
        {
            var isSearch = !(string.IsNullOrEmpty(parameter.Name) && string.IsNullOrEmpty(parameter.Mobile) && string.IsNullOrEmpty(parameter.SerialNumber) && string.IsNullOrEmpty(parameter.Spelling)
                && parameter.Status == 0 && parameter.Deleted == 0 && parameter.DepId > 0);

            this.ExecuteSQL = string.Format("SELECT e.* FROM dbo.employee e INNER JOIN dbo.department d ON e.depid=d.depid WHERE {0} e.name LIKE '%{1}%' AND e.pinyin LIKE '%{2}%' AND e.serialnumber LIKE '%{3}%' AND e.mobile LIKE '%{4}%' AND e.[status] IN({5}) AND e.deleted IN({6}) AND e.comment LIKE '%{7}%' {8}",
                isSearch ? string.Empty : string.Format("e.parentid='{0}' AND", parameter.ParentId),
                parameter.Name,
                parameter.Spelling,
                parameter.SerialNumber,
                parameter.Mobile,
                parameter.Status == (short)CommonStatus.Default ? string.Concat((short)CommonStatus.Used, ",", (short)CommonStatus.Stopped) : parameter.Status.ToString(),
                parameter.Deleted == (short)CommonDeleted.Default ? string.Concat((short)CommonDeleted.Deleted, ",", (short)CommonDeleted.NotDeleted) : parameter.Deleted.ToString(),
                parameter.Comment,
                parameter.DepId > 0 ? string.Format("AND d.depid={0}", parameter.DepId) : string.Empty
                );
            return this.ExecuteReadSqlToemployeeDataModelPageData("empid", parameter.PageIndex, parameter.PageSize, "classid ASC, sort DESC");
        }

        public bool CheckEmployeeIsExist(QueryEmployeeListParameter parameter)
        {
            this.ExecuteSQL = "SELECT * FROM dbo.employee WHERE name=@name AND serialnumber=@serialnumber AND depid=@depid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@name", parameter.Name), 
                new SqlParameter("@serialnumber", parameter.SerialNumber),
                new SqlParameter("@depid", parameter.DepId)
            };
            var employee = this.ExecuteReadSqlToemployeeDataModel();
            return employee != null;
        }

        public List<employeeDataModel> GetEmployeeListByParentID(QueryEmployeeListParameter parameter)
        {
            this.ExecuteSQL = "SELECT * FROM dbo.employee WHERE parentid=@parentid ORDER BY classid DESC";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@parentid", SqlDbType.VarChar){ Value = parameter.ParentId}
            };
            return this.ExecuteReadSqlToemployeeDataModelList();
        }

        public employeeDataModel GetEmployeeByClassID(QueryEmployeeListParameter parameter)
        {
            this.ExecuteSQL = "SELECT * FROM dbo.employee WHERE classid=@classid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@classid", SqlDbType.VarChar){ Value = parameter.ParentId}
            };
            return this.ExecuteReadSqlToemployeeDataModel();
        }

        public int UpdateChildNumberByClassId(IDbTransaction tran, QueryEmployeeListParameter parameter)
        {
            this.ExecuteSQL = "UPDATE dbo.employee SET childnumber=childnumber+1 WHERE classid=@classid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@classid", SqlDbType.VarChar){ Value = parameter.ParentId}
            };
            return this.ExecuteNonQuery(tran);
        }

        public int Delete(DeleteEmployeeParameter parameter)
        {
            this.ExecuteSQL = "UPDATE dbo.employee SET deleted=@deleted WHERE empid=@empid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@deleted", parameter.Deleted), 
                new SqlParameter("@empid", parameter.empid)
            };
            return this.ExecuteNonQuery();
        }

        public int ReDelete(DeleteEmployeeParameter parameter)
        {
            this.ExecuteSQL = "UPDATE dbo.employee SET deleted=@deleted WHERE empid=@empid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@deleted", parameter.Deleted), 
                new SqlParameter("@empid", parameter.empid)
            };
            return this.ExecuteNonQuery();
        }
    }
}