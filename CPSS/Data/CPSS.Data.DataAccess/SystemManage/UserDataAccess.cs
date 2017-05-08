using System;
using System.Data;
using System.Data.SqlClient;
using CPSS.Common.Core.DataAccess.DataAccess;
using CPSS.Common.Core.Paging;
using CPSS.Data.DataAccess.Interfaces.SystemManage.UserManage;
using CPSS.Data.DataAccess.Interfaces.SystemManage.UserManage.Parameters;
using CPSS.Data.DataAcess.DataModels.SystemManage.UserManage;

namespace CPSS.Data.DataAccess.SystemManage
{
    public class UserDataAccess : GenericDataAccessBase<UserDataModel>, IUserDataAccess
    {
        public UserDataAccess(IDbConnection _connection) : base(_connection)
        {
        }

        public PageData<UserDataModel> GetUserPageData(QueryUserParameter parameter)
        {
            this.ExecuteSQL = string.Format("SELECT * FROM dbo.[user] WHERE empid={0} AND username LIKE '%{1}%'", parameter.EmpId, parameter.UserName);
            return this.ExecuteReadSqlToUserDataModelPageData("userid", parameter.PageIndex, parameter.PageSize, "comid ASC,username ASC, deleted ASC");
        }

        public UserDataModel GetUserDataByUserId(QueryUserParameter parameter)
        {
            this.ExecuteSQL = "SELECT * FROM dbo.[user] WHERE userid=@userid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@userid", parameter.userid)
            };
            return this.ExecuteReadSqlToUserDataModel();
        }

        public int AddUser(UserDataModel data)
        {
            this.ExecuteSQL = "INSERT INTO dbo.[user] (comid,empid,username,usepwd,prefix,manager,[status],deleted,synchron,ctime,comment) VALUES  (@comid,@empid,@username,@userpwd,@prefix,@manager,@status,1,0,@ctime,@comment)";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@comid", data.ComId), 
                new SqlParameter("@empid", data.EmpId), 
                new SqlParameter("@username", data.UserName), 
                new SqlParameter("@userpwd", data.UsePwd), 
                new SqlParameter("@prefix", data.Prefix), 
                new SqlParameter("@manager", data.Manager), 
                new SqlParameter("@status", data.Status), 
                new SqlParameter("@ctime", data.CTime), 
                new SqlParameter("@comment", data.Comment)
            };
            return this.ExecuteNonQuery();
        }

        public int UpdateUser(UserDataModel data)
        {
            this.ExecuteSQL = "UPDATE dbo.[user] SET prefix=@prefix,manager=@manager,comment=@comment WHERE userid=@userid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@userid", data.UserId), 
                new SqlParameter("@prefix", data.Prefix), 
                new SqlParameter("@manager", data.Manager), 
                new SqlParameter("@comment", data.Comment)
            };
            return this.ExecuteNonQuery();
        }

        public int ChangeStatus(UserDataModel data)
        {
            this.ExecuteSQL = "UPDATE dbo.[user] SET [status]=@status WHERE userid=@userid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@userid", data.UserId), 
                new SqlParameter("@status", data.Status)
            };
            return this.ExecuteNonQuery();
        }

        public int DeletedUser(UserDataModel data)
        {
            this.ExecuteSQL = "UPDATE dbo.[user] SET [deleted]=@deleted WHERE userid=@userid";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@userid", data.UserId), 
                new SqlParameter("@deleted", data.Deleted)
            };
            return this.ExecuteNonQuery();
        }

    }
}