using System;
using System.Data;
using System.Data.SqlClient;
using CPSS.Common.Helper.Config;

namespace CPSS.Common.Helper.DataAccess
{
    public class CurrentDbConnection
    {
        /// <summary>
        /// 获取或设置要连接到的 SQL Server 实例的名称或网络地址。
        /// </summary>
        public string DataSource { set; private get; }

        /// <summary>
        /// 获取或设置与该连接关联的数据库的名称。
        /// </summary>
        public string InitialCatalog { set; private get; }

        /// <summary>
        /// 获取或设置在终止尝试并产生错误之前，等待与服务器连接的时间长度（以秒为单位）。
        /// </summary>
        public int ConnectTimeout { set; private get; }

        /// <summary>
        /// 获取或设置连接到 SQL Server 时要使用的用户 ID。
        /// </summary>
        public string UserID { set; private get; }

        /// <summary>
        /// 获取或设置 SQL Server 帐户的密码。
        /// </summary>
        public string Password { set; private get; }

        /// <summary>
        /// 获取SqlConnection对象的配置文件对SqlConnection对象进行实例化。
        /// 写死在程序里面，默认路径("~/config/connection.config")。如需修改就改此处的路径即可
        /// </summary>
        private DbConnectionConfig ConnectionConfig {
            get
            {
                var config = ConfigHelper.GetConfig<DbConnectionConfig>("~/config/connection.config");
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
                SqlConnectionStringBuilder connectionBuilder;
                var connectConfig = this.ConnectionConfig;
                if (connectConfig != null)
                {
                    connectionBuilder = new SqlConnectionStringBuilder
                    {
                        DataSource = connectConfig.Server,
                        InitialCatalog = connectConfig.Database,
                        ConnectTimeout = connectConfig.ConnectTimeout,
                        UserID = connectConfig.UserID,
                        Password = connectConfig.Password
                    };
                }
                else
                {
                    if (string.IsNullOrEmpty(this.DataSource)) throw new Exception("DataSource");
                    if (string.IsNullOrEmpty(this.InitialCatalog)) throw new Exception("InitialCatalog");
                    if (this.ConnectTimeout == 0) this.ConnectTimeout = 6000;
                    if (string.IsNullOrEmpty(this.UserID)) throw new Exception("UserID");

                    connectionBuilder = new SqlConnectionStringBuilder
                    {
                        DataSource = this.DataSource,
                        InitialCatalog = this.InitialCatalog,
                        ConnectTimeout = this.ConnectTimeout,
                        UserID = this.UserID,
                        Password = this.Password
                    };
                }
                var connection = new SqlConnection(connectionBuilder.ConnectionString);
                return connection;
            }
        }
    }
}