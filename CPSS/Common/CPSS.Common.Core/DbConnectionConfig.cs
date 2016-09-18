using System;

namespace CPSS.Common.Core
{
    [Serializable]
    public class DbConnectionConfig
    {
        /// <summary>
        /// 数据库服务器
        /// </summary>
        public string Server { set; get; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string Database { set; get; }

        /// <summary>
        /// 连接超时时间(毫秒)
        /// </summary>
        public int ConnectTimeout { set; get; }

        /// <summary>
        /// 连接数据库服务的数据库的账户ID
        /// </summary>
        public string UserID { set; get; }

        /// <summary>
        /// 连接数据库服务的数据库的账户密码
        /// </summary>
        public string Password { set; get; }

        /// <summary>
        /// 生成数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var connection = string.Concat("Server=", this.Server, ";DataBase=", this.Database, ";UID=", this.UserID, ";PWD=", this.Password, ";ConnectTimeout=", this.ConnectTimeout, ";");
            return connection;
        }
    }
}