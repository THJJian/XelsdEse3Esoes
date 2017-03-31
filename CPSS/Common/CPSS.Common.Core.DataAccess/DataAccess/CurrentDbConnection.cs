using System;
using System.Data;
using System.Data.SqlClient;
using CPSS.Common.Core.Authenticate;
using CPSS.Common.Core.Helper.Config;

namespace CPSS.Common.Core.DataAccess.DataAccess
{
    public class CurrentDbConnection
    {

        /// <summary>
        /// 获取SqlConnection对象的配置文件对SqlConnection对象进行实例化。
        /// 写死在程序里面，默认路径("~/config/connection.config")。如需修改就改此处的路径即可
        /// </summary>
        private DbConnectionConfig ConnectionConfig {
            get
            {
                var connectFilePath = "~/config/connection.config";
                if (CPSSAuthenticate.NotUseMainConnection())
                {
                    var user = CPSSAuthenticate.GetCurrentUser();
                    if (user != null && user.UserID > 0)
                        connectFilePath = string.Format("~/config/{0}/connection.config", user.CompanySerialNum);
                }
                var config = ConfigHelper.GetConfig<DbConnectionConfig>(connectFilePath);
                return config;
            }
        }

        /// <summary>
        /// 创建连接对象
        /// </summary>
        public IDbConnection BuildConnection
        {
            get
            {
                var connectConfig = ConnectionConfig;
                if (connectConfig == null) throw new Exception("数据库连接配置文件出错。");
                var connectionBuilder = new SqlConnectionStringBuilder
                {
                    DataSource = connectConfig.Server,
                    InitialCatalog = connectConfig.Database,
                    ConnectTimeout = connectConfig.ConnectTimeout,
                    UserID = connectConfig.UserID,
                    Password = connectConfig.Password
                };
                var connection = new SqlConnection(connectionBuilder.ConnectionString);
                return connection;
            }
        }
    }
}