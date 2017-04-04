using System;
using System.Data;
using System.Data.SqlClient;
using CPSS.Common.Core.DataAccess.DataAccess;
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
            this.ExecuteSQL = "SELECT userid, username, userpwd, comid, comname,issystem,ismanager FROM sysuser WHERE comid=@comid AND username=@username AND userpwd=@userpwd";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@username", parameter.UserName), 
                new SqlParameter("@userpwd", parameter.UserPwd), 
                new SqlParameter("@comid", parameter.CompanySerialNum)
            };
            return this.ExecuteReadSqlToSigninUserDataModel();
        }

        public bool SaveLoginUserToOnline(OnlineSigninUserParameter parameter)
        {
            this.ExecuteSQL = "INSERT INTO dbo.Online(userid, username, userip, browser, logintime, overtime, exptime, status, sguid) VALUES (@userid, @username, @userip, @browser, @logintime, @overtime, @exptime, 0, @sguid)";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@userid", parameter.UserID),
                new SqlParameter("@username", parameter.LoginName), 
                new SqlParameter("@userip", parameter.UserIP),
                new SqlParameter("@browser", parameter.Browser),
                new SqlParameter("@logintime", parameter.LoginTime),
                new SqlParameter("@overtime", parameter.OverTime),
                new SqlParameter("@exptime", parameter.ExpTime),
                new SqlParameter("@sguid", parameter.SGuid)
            };
            return this.ExecuteNonQuery() > 0;
        }

        public OnlineSigninUserDataModel GetOnlineSigninUserByUserID_g(OnlineSigninUserParameter parameter)
        {
            var nowDate = DateTime.Now;
            this.ExecuteSQL = "SELECT * FROM [online] WHERE sguid=@sguid AND userip=@userip AND overtime<@overtime AND status=0";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@sguid", parameter.SGuid),
                new SqlParameter("@userip", parameter.UserIP),
                new SqlParameter("@overtime", new DateTime(nowDate.Year, nowDate.Month, nowDate.Day).AddDays(1))
            };
            return this.ExecuteReadSqlToOnlineSigninUserDataModel();
        }

        public SigninUserDataModel FindSininUserDataModelByUserID(OnlineSigninUserParameter parameter)
        {
            this.ExecuteSQL = "SELECT * FROM sysuser WHERE UserID=@UserID";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@UserID", parameter.UserID)
            };
            return this.ExecuteReadSqlToSigninUserDataModel();
        }
    }
}