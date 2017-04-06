using CPSS.Common.Core.Paging;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters;
using CPSS.Data.DataAcess.DataModels;

namespace CPSS.Data.DataAccess.Interfaces
{
    public partial interface IsubcompanyDataAccess
    {
        PageData<subcompanyDataModel> GetQuerySubCompanyList(QuerySubCompanyListParameter parameter);
    }
}