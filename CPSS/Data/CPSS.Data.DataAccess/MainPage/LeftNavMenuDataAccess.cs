using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CPSS.Common.Core.DataAccess;
using CPSS.Data.DataAccess.Interfaces.MainPage;
using CPSS.Data.DataAccess.Interfaces.MainPage.Parameters;
using CPSS.Data.DataAcess.DataModels.MainPage;

namespace CPSS.Data.DataAccess.MainPage
{
    public class LeftNavMenuDataAccess : GenericDataAccessBase<LeftNavMenuDataModel>, ILeftNavMenuDataAccess
    {
        public LeftNavMenuDataAccess(IDbConnection connection) : base(connection)
        {
        }

        public List<LeftNavMenuDataModel> GetLeftNavMenuDataModels(LeftNavMenuParameter parameter)
        {
            //需要联查权限表
            this.ExecuteSQL = "SELECT * FROM sys_meus WHERE UserID=@UserID";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@UserID", parameter.UserID)
            };
            return this.ExecuteReadSqlToLeftNavMenuDataModelList();
        }
    }
}