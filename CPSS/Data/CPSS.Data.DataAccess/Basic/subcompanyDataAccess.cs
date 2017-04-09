using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CPSS.Common.Core.Paging;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters;
using CPSS.Data.DataAcess.DataModels;

namespace CPSS.Data.DataAccess
{
    public partial class subcompanyDataAccess
    {
        public PageData<subcompanyDataModel> GetQuerySubCompanyList(QuerySubCompanyListParameter parameter)
        {
            this.ExecuteSQL = string.Format("SELECT * FROM dbo.subcompany WHERE parentid='{0}' AND serialnumber LIKE '%{1}%' AND name LIKE '%{2}%' AND pinyin LIKE '%{3}%' AND email LIKE '%{4}%' AND linkman LIKE '%{5}%' AND linktel LIKE '%{6}%' AND [status]={7} AND pricemode in({8})"
                    , parameter.ParentId
                    , parameter.SerialNumber
                    , parameter.Name
                    , parameter.Spelling
                    , parameter.Email
                    , parameter.LinkMan
                    , parameter.LinkTel
                    , parameter.Status
                    , parameter.PriceMode == 0 ? "1,2,3" : parameter.PriceMode.ToString());
            return this.ExecuteReadSqlTosubcompanyDataModelPageData("subcomid", parameter.PageIndex, parameter.PageSize, "[sort] ASC");
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
    }
}