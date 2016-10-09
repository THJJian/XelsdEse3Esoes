using System.Data;
using System.Data.SqlClient;
using CPSS.Common.Core.DataAccess;
using CPSS.Common.Core.DataAccess.DataAccess;
using CPSS.Data.DataAccess.Interfaces.User;
using CPSS.Data.DataAccess.Interfaces.User.Parameters;
using CPSS.Data.DataAcess.DataModels.User;

namespace CPSS.Data.DataAccess.User
{
    public class CompanyInfoDataAccess : GenericDataAccessBase<CompanyInfoDataModel>, ICompanyInfoDataAccess
    {
        public CompanyInfoDataAccess(IDbConnection _connection) : base(_connection)
        {
        }
        
        public bool AddCompanyInfo(CompanyInfoDataModel parameter)
        {
            this.ExecuteSQL = "INSERT INTO CompnayInfo(CompanyID,Server,Database,ConnectTimeout,UserID,Password) VALUES(@CompanyID,@Server,@Database,@ConnectTimeout,@UserID,@Password)";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@CompanyID", parameter.CompanyID),
                new SqlParameter("@Server", parameter.Server),
                new SqlParameter("@Database", parameter.Database),
                new SqlParameter("@ConnectTimeout", parameter.ConnectTimeout),
                new SqlParameter("@UserID", parameter.UserID),
                new SqlParameter("@Password", parameter.Password)
            };
            return this.ExecuteNonQuery() > 0;
        }

        public CompanyInfoDataModel GetCompanyInfoDataModelByID(CompanyInfoParameter parameter)
        {
            this.ExecuteSQL = "SELECT * FROM CompnayInfo WHERE CompanyID=@CompanyID";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@CompanyID", parameter.CompanyID) 
            };
            return this.ExecuteReadSqlToCompanyInfoDataModel();
        }

    }
}