using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CPSS.Common.Core.Paging;
using CPSS.Common.Core.Type.EnumType;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters.SubCompany;
using CPSS.Data.DataAcess.DataModels;

namespace CPSS.Data.DataAccess
{
    public partial class subcompanyDataAccess
    {
        public PageData<subcompanyDataModel> GetQuerySubCompanyList(QuerySubCompanyListParameter parameter)
        {
            var isSearch = !(string.IsNullOrEmpty(parameter.Email) && string.IsNullOrEmpty(parameter.LinkMan) && string.IsNullOrEmpty(parameter.LinkTel)
                && string.IsNullOrEmpty(parameter.Name) && string.IsNullOrEmpty(parameter.SerialNumber) && string.IsNullOrEmpty(parameter.Spelling)
                && parameter.PriceMode == 0 && parameter.Status == (short)CommonStatus.Default);

            this.ExecuteSQL = string.Format("SELECT * FROM dbo.subcompany WHERE {0} serialnumber LIKE '%{1}%' AND name LIKE '%{2}%' AND pinyin LIKE '%{3}%' AND email LIKE '%{4}%' AND linkman LIKE '%{5}%' AND linktel LIKE '%{6}%' AND [status] IN({7}) AND pricemode in({8}) AND deleted IN({9})"
                , isSearch ? string.Empty : string.Format(" parentid='{0}' AND ", parameter.ParentId)
                , parameter.SerialNumber
                , parameter.Name
                , parameter.Spelling
                , parameter.Email
                , parameter.LinkMan
                , parameter.LinkTel
                , parameter.Status == (short)CommonStatus.Default ? string.Concat((short)CommonStatus.Used, ",", (short)CommonStatus.Stopped) : parameter.Status.ToString()
                , parameter.PriceMode == 0 ? "1,2,3" : parameter.PriceMode.ToString()
                , parameter.Deleted == (short)CommonDeleted.Default ? string.Concat((short)CommonDeleted.Deleted, ",", (short)CommonDeleted.NotDeleted) : parameter.Deleted.ToString());
            return this.ExecuteReadSqlTosubcompanyDataModelPageData("subcomid", parameter.PageIndex, parameter.PageSize, "classid ASC, [sort] DESC");
        }

        public List<subcompanyDataModel> GetSubCompanyListByParentID(QuerySubCompanyListParameter parameter)
        {
            this.ExecuteSQL = "SELECT  * FROM dbo.subcompany WHERE parentid=@parentid ORDER BY classid DESC";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@parentid", SqlDbType.VarChar){ Value = parameter.ParentId}
            };
            return this.ExecuteReadSqlTosubcompanyDataModelList();
        }

        public subcompanyDataModel GetSubCompanyByClassID(QuerySubCompanyListParameter parameter)
        {
            this.ExecuteSQL = "SELECT * FROM dbo.subcompany WHERE classid=@classid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@classid", SqlDbType.VarChar){ Value = parameter.ParentId}
            };
            return this.ExecuteReadSqlTosubcompanyDataModel();
        }

        public int UpdateChildNumberByClassId(IDbTransaction tran, QuerySubCompanyListParameter parameter)
        {
            this.ExecuteSQL = "UPDATE dbo.subcompany SET childnumber=childnumber+1 WHERE classid=@classid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@classid", SqlDbType.VarChar){ Value = parameter.ParentId}
            };
            return this.ExecuteNonQuery(tran);
        }

        public int Delete(DeleteSubCompanyParameter parameter)
        {
            this.ExecuteSQL = "UPDATE dbo.subcompany SET deleted=@deleted WHERE subcomid=@subcomid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@deleted", parameter.Deleted), 
                new SqlParameter("@subcomid", parameter.ComId)
            };
            return this.ExecuteNonQuery();
        }

        public int ReDelete(DeleteSubCompanyParameter parameter)
        {
            this.ExecuteSQL = "UPDATE dbo.subcompany SET deleted=@deleted WHERE subcomid=@subcomid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@deleted", parameter.Deleted), 
                new SqlParameter("@subcomid", parameter.ComId)
            };
            return this.ExecuteNonQuery();
        }
    }
}