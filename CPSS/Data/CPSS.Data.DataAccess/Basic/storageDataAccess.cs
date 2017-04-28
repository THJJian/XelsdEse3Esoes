using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CPSS.Common.Core.Paging;
using CPSS.Common.Core.Type.EnumType;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters.Storage;
using CPSS.Data.DataAcess.DataModels;

namespace CPSS.Data.DataAccess
{
    public partial class storageDataAccess
    {
        public PageData<storageDataModel> GetQueryStorageList(QueryStorageListParameter parameter)
        {
            var isSearch = !(string.IsNullOrEmpty(parameter.SerialNumber) && string.IsNullOrEmpty(parameter.Name) && string.IsNullOrEmpty(parameter.Spelling) && string.IsNullOrEmpty(parameter.Alias)
                && parameter.Status == 0 && parameter.Deleted == 0);

            this.ExecuteSQL = string.Format("SELECT * FROM dbo.storage WHERE {0} serialnumber LIKE '%{1}%' AND name LIKE '%{2}%' AND pinyin LIKE '%{3}%' AND alias LIKE '%{4}%' AND [status] IN({5}) AND deleted IN({6})",
                isSearch ? string.Empty : string.Format("parentid='{0}' AND", parameter.ParentId),
                parameter.SerialNumber,
                parameter.Name,
                parameter.Spelling,
                parameter.Alias,
                parameter.Status == (short)CommonStatus.Default ? string.Concat((short)CommonStatus.Used, ",", (short)CommonStatus.Stopped) : parameter.Status.ToString(),
                parameter.Deleted == (short)CommonDeleted.Default ? string.Concat((short)CommonDeleted.Deleted, ",", (short)CommonDeleted.NotDeleted) : parameter.Deleted.ToString()
                );
            return this.ExecuteReadSqlTostorageDataModelPageData("stoid", parameter.PageIndex, parameter.PageSize, "classid ASC, sort DESC");
        }

        public bool CheckStorageIsExist(QueryStorageListParameter parameter)
        {
            this.ExecuteSQL = "SELECT * FROM dbo.storage WHERE name=@name AND serialnumber=@serialnumber";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@name", parameter.Name), 
                new SqlParameter("@serialnumber", parameter.SerialNumber)
            };
            var storage = this.ExecuteReadSqlTostorageDataModel();
            return storage != null;
        }

        public List<storageDataModel> GetStorageListByParentID(QueryStorageListParameter parameter)
        {
            this.ExecuteSQL = "SELECT * FROM dbo.storage WHERE parentid=@parentid ORDER BY classid DESC";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@parentid", SqlDbType.VarChar){ Value = parameter.ParentId}
            };
            return this.ExecuteReadSqlTostorageDataModelList();
        }

        public storageDataModel GetStorageByClassID(QueryStorageListParameter parameter)
        {
            this.ExecuteSQL = "SELECT * FROM dbo.storage WHERE classid=@classid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@classid", SqlDbType.VarChar){ Value = parameter.ParentId}
            };
            return this.ExecuteReadSqlTostorageDataModel();
        }

        public int UpdateChildNumberByClassId(IDbTransaction tran, QueryStorageListParameter parameter)
        {
            this.ExecuteSQL = "UPDATE dbo.storage SET childnumber=childnumber+1 WHERE classid=@classid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@classid", SqlDbType.VarChar){ Value = parameter.ParentId}
            };
            return this.ExecuteNonQuery(tran);
        }

        public int Delete(DeleteStorageParameter parameter)
        {
            this.ExecuteSQL = "UPDATE dbo.storage SET deleted=@deleted WHERE stoid=@stoid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@deleted", parameter.Deleted), 
                new SqlParameter("@stoid", parameter.StorageId)
            };
            return this.ExecuteNonQuery();
        }

        public int ReDelete(DeleteStorageParameter parameter)
        {
            this.ExecuteSQL = "UPDATE dbo.storage SET deleted=@deleted WHERE stoid=@stoid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@deleted", parameter.Deleted), 
                new SqlParameter("@stoid", parameter.StorageId)
            };
            return this.ExecuteNonQuery();
        }
    }
}