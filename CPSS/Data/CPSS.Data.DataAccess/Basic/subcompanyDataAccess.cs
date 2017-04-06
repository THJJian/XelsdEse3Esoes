using CPSS.Common.Core.Paging;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters;
using CPSS.Data.DataAcess.DataModels;

namespace CPSS.Data.DataAccess
{
    public partial class subcompanyDataAccess
    {
        public PageData<subcompanyDataModel> GetQuerySubCompanyList(QuerySubCompanyListParameter parameter)
        {
            this.ExecuteSQL = string.Format("SELECT * FROM dbo.subcompany WHERE parentid='{0}' AND serialnumber LIKE '%{1}%' AND name LIKE '%{2}%' AND pinyin LIKE '%{3}%' AND email LIKE '%{4}%' AND linkman LIKE '%{5}%' AND linktel LIKE '%{6}%' AND [status]={7} AND pricemode={8}"
                ,parameter.ParentId
                ,parameter.SerialNumber
                ,parameter.Name
                ,parameter.Spelling
                ,parameter.Email
                ,parameter.LinkMan
                ,parameter.LinkTel
                ,parameter.Status
                ,parameter.PriceMode);
            return this.ExecuteReadSqlTosubcompanyDataModelPageData("subcomid", parameter.PageIndex, parameter.PageSize, "[sort] ASC");
        }
    }
}