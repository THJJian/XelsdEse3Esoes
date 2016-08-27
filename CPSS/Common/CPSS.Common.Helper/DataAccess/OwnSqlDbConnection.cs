// #region << 版 本 注 释 >>
// /*
//  * ========================================================================
//  * 文件名：  OwnSqlDbConnection.cs 
//  * 创建人：  唐洪建 
//  * 创建时间：2016-08-02 13:21
//  * 版本号：  V1.0.0.0 
//  * 描述：
//  * =====================================================================
//  * 修改人：
//  * 修改时间：2016-08-02 13:21
//  * 版本号： V1.0.0.1
//  * 描述：
// */
// #endregion

using System.Data;
using System.Data.SqlClient;
using CPSS.Common.Helper.Config;

namespace CPSS.Common.Helper.DataAccess
{
    /// <summary>
    /// 自定义实现数据库连接对象类。
    /// 方便从单独的数据库连接对象读取配置信息
    /// </summary>
    public class OwnSqlDbConnection : IDbConnection
    {
        private readonly IDbConnection sqlDbConnection;

        public OwnSqlDbConnection()
        {
            this.sqlDbConnection = new CurrentDbConnection().BuildConnection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectFilePath">数据库连接配置文件存放路径</param>
        public OwnSqlDbConnection(string connectFilePath)
        {
            var dbConnectConfig = ConfigHelper.GetConfig<DbConnectionConfig>(connectFilePath);
            var result = new SqlConnectionStringBuilder
            {
                DataSource = dbConnectConfig.Server,
                InitialCatalog = dbConnectConfig.Database,
                ConnectTimeout = dbConnectConfig.ConnectTimeout,
                UserID = dbConnectConfig.UserID,
                Password = dbConnectConfig.Password
            };
            this.sqlDbConnection = new SqlConnection(result.ConnectionString);
        }

        /// <summary>
        /// 以指定的 System.Data.IsolationLevel 值开始一个数据库事务。
        /// </summary>
        /// <param name="il"></param>
        /// <returns></returns>
        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return this.sqlDbConnection.BeginTransaction(il);
        }

        /// <summary>
        /// 开始数据库事务。
        /// </summary>
        /// <returns></returns>
        public IDbTransaction BeginTransaction()
        {
            return this.sqlDbConnection.BeginTransaction();
        }

        /// <summary>
        /// 为打开的 Connection 对象更改当前数据库。
        /// </summary>
        /// <param name="databaseName"></param>
        public void ChangeDatabase(string databaseName)
        {
            this.sqlDbConnection.ChangeDatabase(databaseName);
        }

        /// <summary>
        /// 关闭与数据库的连接。
        /// </summary>
        public void Close()
        {
            this.sqlDbConnection.Close();
        }

        /// <summary>
        /// 获取或设置用于打开数据库的字符串。
        /// </summary>
        public string ConnectionString
        {
            get { return this.sqlDbConnection.ConnectionString; }
            set { this.sqlDbConnection.ConnectionString = value; }
        }

        /// <summary>
        /// 获取在尝试建立连接时终止尝试并生成错误之前所等待的时间。
        /// </summary>
        public int ConnectionTimeout
        {
            get { return this.sqlDbConnection.ConnectionTimeout; }
        }

        /// <summary>
        /// 创建并返回一个与该连接相关联的 Command 对象。
        /// </summary>
        /// <returns></returns>
        public IDbCommand CreateCommand()
        {
            return this.sqlDbConnection.CreateCommand();
        }

        /// <summary>
        /// 获取当前数据库或连接打开后要使用的数据库的名称。
        /// </summary>
        public string Database
        {
            get { return this.sqlDbConnection.Database; }
        }

        /// <summary>
        ///  打开一个数据库连接，其设置由提供程序特定的 Connection 对象的 ConnectionString 属性指定。
        /// </summary>
        public void Open()
        {
            this.sqlDbConnection.Open();
        }

        /// <summary>
        /// 获取连接的当前状态。
        /// </summary>
        public ConnectionState State
        {
            get { return this.sqlDbConnection.State; }
        }

        public void Dispose()
        {
            this.sqlDbConnection.Dispose();
        }
    }
}