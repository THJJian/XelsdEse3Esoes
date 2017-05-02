using System.Collections.Generic;
using System.Data;
using CPSS.Common.Core.Paging;
using CPSS.Common.Core.Type.EnumType;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters.Client;
using CPSS.Data.DataAcess.DataModels;
using System.Data.SqlClient;

namespace CPSS.Data.DataAccess
{
    public partial class clientDataAccess
    {
        public PageData<clientDataModel> GetQueryClientList(QueryClientListParameter parameter)
        {
            var isSearch = !(string.IsNullOrEmpty(parameter.SerialNumber) && string.IsNullOrEmpty(parameter.Name) && string.IsNullOrEmpty(parameter.Spelling) && string.IsNullOrEmpty(parameter.Alias)
                && string.IsNullOrEmpty(parameter.LinkMan) && string.IsNullOrEmpty(parameter.LinkTel) && parameter.PriceMode == 0 && parameter.Status == 0 && parameter.Deleted == 0);

            this.ExecuteSQL = string.Format("SELECT * FROM dbo.client WHERE {0} serialnumber LIKE '%{1}%' AND name LIKE '%{2}%' AND pinyin LIKE '%{3}%' AND alias LIKE '%{4}%' AND linkman LIKE '%{5}%' AND linktel LIKE '%{6}%' AND pricemode IN({7}) AND [status] IN({8}) AND deleted IN({9})",
                isSearch ? string.Empty : string.Format("parentid='{0}' AND", parameter.ParentId),
                parameter.SerialNumber,
                parameter.Name,
                parameter.Spelling,
                parameter.Alias,
                parameter.LinkMan,
                parameter.LinkTel,
                parameter.PriceMode == 0 ? "1,2,3" : parameter.PriceMode.ToString(),
                parameter.Status == (short)CommonStatus.Default ? string.Concat((short)CommonStatus.Used, ",", (short)CommonStatus.Stopped) : parameter.Status.ToString(),
                parameter.Deleted == (short)CommonDeleted.Default ? string.Concat((short)CommonDeleted.Deleted, ",", (short)CommonDeleted.NotDeleted) : parameter.Deleted.ToString()
                );
            return this.ExecuteReadSqlToclientDataModelPageData("clientid", parameter.PageIndex, parameter.PageSize, "classid ASC, sort DESC");
        }

        public bool CheckClientIsExist(QueryClientListParameter parameter)
        {
            this.ExecuteSQL = string.Format("SELECT * FROM dbo.client WHERE name=@name AND serialnumber=@serialnumber{0}", parameter.ClientId == 0 ? string.Empty : string.Concat(" AND clientid<>", parameter.ClientId));
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@name", parameter.Name), 
                new SqlParameter("@serialnumber", parameter.SerialNumber)
            };
            var client = this.ExecuteReadSqlToclientDataModel();
            return client != null;
        }

        public List<clientDataModel> GetClientListByParentID(QueryClientListParameter parameter)
        {
            this.ExecuteSQL = "SELECT * FROM dbo.client WHERE parentid=@parentid ORDER BY classid DESC";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@parentid", SqlDbType.VarChar){ Value = parameter.ParentId}
            };
            return this.ExecuteReadSqlToclientDataModelList();
        }

        public clientDataModel GetClientByClassID(QueryClientListParameter parameter)
        {
            this.ExecuteSQL = "SELECT * FROM dbo.client WHERE classid=@classid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@classid", SqlDbType.VarChar){ Value = parameter.ParentId}
            };
            return this.ExecuteReadSqlToclientDataModel();
        }

        public int UpdateChildNumberByClassId(IDbTransaction tran, QueryClientListParameter parameter)
        {
            this.ExecuteSQL = "UPDATE dbo.client SET childnumber=childnumber+1 WHERE classid=@classid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@classid", SqlDbType.VarChar){ Value = parameter.ParentId}
            };
            return this.ExecuteNonQuery(tran);
        }

        public int Delete(DeleteClientParameter parameter)
        {
            this.ExecuteSQL = "UPDATE dbo.client SET deleted=@deleted WHERE clientid=@clientid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@deleted", parameter.Deleted), 
                new SqlParameter("@clientid", parameter.ClientId)
            };
            return this.ExecuteNonQuery();
        }

        public int ReDelete(DeleteClientParameter parameter)
        {
            this.ExecuteSQL = "UPDATE dbo.client SET deleted=@deleted WHERE clientid=@clientid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@deleted", parameter.Deleted), 
                new SqlParameter("@clientid", parameter.ClientId)
            };
            return this.ExecuteNonQuery();
        }
    }
}