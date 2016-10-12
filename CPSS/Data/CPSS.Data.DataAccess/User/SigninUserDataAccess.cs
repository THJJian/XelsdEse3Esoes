﻿using System;
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
            this.ExecuteSQL = "SELECT UserID,CompanySerialNum,CompanyName,UserName,CASE WHEN Manager=1 AND isSystem=1 THEN 1 ELSE 0 END IsSysManager FROM sys_users WHERE UserName=@UserName AND ISNULL(UserPwd,'')=@UserPwd AND CompanySerialNum=@CompanySerialNum AND Deleted = 0";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@UserName", parameter.UserName), 
                new SqlParameter("@UserPwd", parameter.UserPwd), 
                new SqlParameter("@CompanySerialNum", parameter.CompanySerialNum)
            };
            return this.ExecuteReadSqlToSigninUserDataModel();
        }

        public bool SaveLoginUserToOnline(OnlineSigninUserParameter parameter)
        {
            this.ExecuteSQL = "INSERT INTO dbo.Online(UserID, LoginName, UserIP, Browser, LoginTime, OverTime, ExpTime, StateID, SGuid) VALUES (@UserID, @LoginName, @UserIP, @Browser, @LoginTime, @OverTime, @ExpTime, 0, @SGuid)";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@UserID", parameter.UserID),
                new SqlParameter("@LoginName", parameter.LoginName), 
                new SqlParameter("@UserIP", parameter.UserIP),
                new SqlParameter("@Browser", parameter.Browser),
                new SqlParameter("@LoginTime", parameter.LoginTime),
                new SqlParameter("@OverTime", parameter.OverTime),
                new SqlParameter("@ExpTime", parameter.ExpTime),
                new SqlParameter("@SGuid", parameter.SGuid)
            };
            return this.ExecuteNonQuery() > 0;
        }

        public OnlineSigninUserDataModel GetOnlineSigninUserByUserID_g(OnlineSigninUserParameter parameter)
        {
            var nowDate = DateTime.Now;
            this.ExecuteSQL = "SELECT * FROM [Online] WHERE SGuid=@SGuid AND UserIP=@UserIP AND OverTime<@OverTime AND StateID=0";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@SGuid", parameter.SGuid),
                new SqlParameter("@UserIP", parameter.UserIP),
                new SqlParameter("@OverTime", new DateTime(nowDate.Year, nowDate.Month, nowDate.Day).AddDays(1))
            };
            return this.ExecuteReadSqlToOnlineSigninUserDataModel();
        }

        public SigninUserDataModel FindSininUserDataModelByUserID(OnlineSigninUserParameter parameter)
        {
            this.ExecuteSQL = "SELECT * FROM sys_users WHERE UserID=@UserID";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@UserID", parameter.UserID)
            };
            return this.ExecuteReadSqlToSigninUserDataModel();
        }
    }
}