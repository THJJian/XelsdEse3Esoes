using System.Collections.Generic;
using System.Data;
using CPSS.Common.Core.Paging;
using CPSS.Common.Core.Type.EnumType;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters.Product;
using CPSS.Data.DataAcess.DataModels;
using System.Data.SqlClient;

namespace CPSS.Data.DataAccess
{
    public partial class productDataAccess
    {
        public PageData<productDataModel> GetQueryProductList(QueryProductListParameter parameter)
        {
            var isSearch = !(string.IsNullOrEmpty(parameter.Name) && string.IsNullOrEmpty(parameter.SerialNumber) && string.IsNullOrEmpty(parameter.Spelling)
                && parameter.Status == 0 && parameter.Deleted == 0);

            this.ExecuteSQL = string.Format("SELECT e.* FROM dbo.product e INNER JOIN dbo.department d ON e.depid=d.depid WHERE {0} e.name LIKE '%{1}%' AND e.pinyin LIKE '%{2}%' AND e.serialnumber LIKE '%{3}%' AND e.[status] IN({4}) AND e.deleted IN({5})",
                isSearch ? string.Empty : string.Format("e.parentid='{0}' AND", parameter.ParentId),
                parameter.Name,
                parameter.Spelling,
                parameter.SerialNumber,
                parameter.Status == (short)CommonStatus.Default ? string.Concat((short)CommonStatus.Used, ",", (short)CommonStatus.Stopped) : parameter.Status.ToString(),
                parameter.Deleted == (short)CommonDeleted.Default ? string.Concat((short)CommonDeleted.Deleted, ",", (short)CommonDeleted.NotDeleted) : parameter.Deleted.ToString()
                );
            return this.ExecuteReadSqlToproductDataModelPageData("empid", parameter.PageIndex, parameter.PageSize, "classid ASC, sort DESC");
        }

        public bool CheckProductIsExist(QueryProductListParameter parameter)
        {
            this.ExecuteSQL = "SELECT * FROM dbo.product WHERE name=@name AND serialnumber=@serialnumber";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@name", parameter.Name), 
                new SqlParameter("@serialnumber", parameter.SerialNumber)
            };
            var product = this.ExecuteReadSqlToproductDataModel();
            return product != null;
        }

        public List<productDataModel> GetProductListByParentID(QueryProductListParameter parameter)
        {
            this.ExecuteSQL = "SELECT * FROM dbo.product WHERE parentid=@parentid ORDER BY classid DESC";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@parentid", SqlDbType.VarChar){ Value = parameter.ParentId}
            };
            return this.ExecuteReadSqlToproductDataModelList();
        }

        public productDataModel GetProductByClassID(QueryProductListParameter parameter)
        {
            this.ExecuteSQL = "SELECT * FROM dbo.product WHERE classid=@classid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@classid", SqlDbType.VarChar){ Value = parameter.ParentId}
            };
            return this.ExecuteReadSqlToproductDataModel();
        }

        public int UpdateChildNumberByClassId(IDbTransaction tran, QueryProductListParameter parameter)
        {
            this.ExecuteSQL = "UPDATE dbo.product SET childnumber=childnumber+1 WHERE classid=@classid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@classid", SqlDbType.VarChar){ Value = parameter.ParentId}
            };
            return this.ExecuteNonQuery(tran);
        }

        public int Delete(DeleteProductParameter parameter)
        {
            this.ExecuteSQL = "UPDATE dbo.product SET deleted=@deleted WHERE empid=@proid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@deleted", parameter.Deleted), 
                new SqlParameter("@proid", parameter.Proid)
            };
            return this.ExecuteNonQuery();
        }

        public int ReDelete(DeleteProductParameter parameter)
        {
            this.ExecuteSQL = "UPDATE dbo.product SET deleted=@deleted WHERE empid=@proid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@deleted", parameter.Deleted), 
                new SqlParameter("@proid", parameter.Proid)
            };
            return this.ExecuteNonQuery();
        }
    }
}