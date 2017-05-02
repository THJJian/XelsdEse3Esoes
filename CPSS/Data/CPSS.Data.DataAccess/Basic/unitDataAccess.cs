using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CPSS.Common.Core.Paging;
using CPSS.Common.Core.Type.EnumType;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters.Unit;
using CPSS.Data.DataAcess.DataModels;

namespace CPSS.Data.DataAccess
{
    public partial class unitDataAccess
    {
        public PageData<unitDataModel> GetQueryUnitList(QueryUnitListParameter parameter)
        {
            this.ExecuteSQL = string.Format("SELECT * FROM dbo.unit WHERE name LIKE '%{0}%' AND [status] IN({1}) AND deleted IN({2})",
                parameter.Name,
                parameter.Status == (short)CommonStatus.Default ? string.Concat((short)CommonStatus.Used, ",", (short)CommonStatus.Stopped) : parameter.Status.ToString(),
                parameter.Deleted == (short)CommonDeleted.Default ? string.Concat((short)CommonDeleted.Deleted, ",", (short)CommonDeleted.NotDeleted) : parameter.Deleted.ToString()
                );
            return this.ExecuteReadSqlTounitDataModelPageData("unitid", parameter.PageIndex, parameter.PageSize, "[sort] DESC");
        }

        public bool CheckUnitIsExist(QueryUnitListParameter parameter)
        {
            this.ExecuteSQL = string.Format("SELECT * FROM dbo.unit WHERE name=@name{0}", parameter.UnitId == 0 ? string.Empty : string.Concat(" AND unitid<>", parameter.UnitId));
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@name", parameter.Name)
            };
            var unit = this.ExecuteReadSqlTounitDataModel();
            return unit != null;
        }

        public int Delete(DeleteUnitParameter parameter)
        {
            this.ExecuteSQL = "UPDATE dbo.unit SET deleted=@deleted WHERE unitid=@unitid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@deleted", parameter.Deleted), 
                new SqlParameter("@unitid", parameter.UnitId)
            };
            return this.ExecuteNonQuery();
        }

        public int ReDelete(DeleteUnitParameter parameter)
        {
            this.ExecuteSQL = "UPDATE dbo.unit SET deleted=@deleted WHERE unitid=@unitid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@deleted", parameter.Deleted), 
                new SqlParameter("@unitid", parameter.UnitId)
            };
            return this.ExecuteNonQuery();
        }

        public List<unitDataModel> GetAllUnit(QueryUnitListParameter parameter)
        {
            this.ExecuteSQL = string.Format("SELECT * FROM dbo.unit WHERE name LIKE '%{0}%' AND ISNULL(parentid,'')<>'' AND [status]={1} AND deleted={2}",
                parameter.Name,
                (short)CommonStatus.Used,
                (short)CommonDeleted.NotDeleted);
            return this.ExecuteReadSqlTounitDataModelList();
        }
    }
}