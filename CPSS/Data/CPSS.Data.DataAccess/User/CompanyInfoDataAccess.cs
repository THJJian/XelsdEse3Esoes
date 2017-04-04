using System.Data;
using System.Data.SqlClient;
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
            this.ExecuteSQL = "INSERT INTO [compnaydbinfo](comid,dbserver,dbase,timeout,uid,pwd) VALUES(@comid,@dbserver,@dbase,@timeout,@uid,@pwd)";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@comid", parameter.comid),
                new SqlParameter("@dbserver", parameter.dbserver),
                new SqlParameter("@dbase", parameter.dbase),
                new SqlParameter("@timeout", parameter.timeout),
                new SqlParameter("@uid", parameter.uid),
                new SqlParameter("@pwd", parameter.pwd)
            };
            return this.ExecuteNonQuery() > 0;
        }

        public CompanyInfoDataModel GetCompanyInfoDataModelByID(CompanyInfoParameter parameter)
        {
            this.ExecuteSQL = "SELECT * FROM [compnaydbinfo] WHERE comid=@comid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@comid", parameter.CompanyID) 
            };
            return this.ExecuteReadSqlToCompanyInfoDataModel();
        }

    }
}