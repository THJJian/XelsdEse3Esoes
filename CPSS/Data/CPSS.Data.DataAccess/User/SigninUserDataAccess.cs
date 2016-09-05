using System.Data;
using System.Data.SqlClient;
using CPSS.Common.Core.DataAccess;
using CPSS.Data.DataAccess.Interfaces.User;
using CPSS.Data.DataAcess.DataModels.User;
using CPSS.Data.DataAccess.Interfaces.User.Parameters;

namespace CPSS.Data.DataAccess.User
{
    public class SigninUserDataAccess : GenericDataAccessBase<SigninUserDataModel>, ISiginUserDataAccess
    {
        public SigninUserDataAccess(IDbConnection _connection) : base(_connection)
        {
        }

        public SigninUserDataModel QuerySigninUserDataModel(SigninUserParameter parameter)
        {
            this.ExecuteSQL = "SELECT * FROM Users WHERE UserName=@UserName AND UserPwd=@UserPwd AND CompanySerialNum=@CompanySerialNum";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@UserName", parameter.UserName), 
                new SqlParameter("@UserPwd", parameter.UserPwd), 
                new SqlParameter("@CompanySerialNum", parameter.CompanySerialNum)
            };
            return this.ExecuteReadSqlToSigninUserDataModel();
        }
    }
}