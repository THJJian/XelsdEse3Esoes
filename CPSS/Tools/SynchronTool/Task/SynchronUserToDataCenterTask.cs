using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using CPSS.Common.Core;
using SynchronTool.ConfigEntity;

namespace SynchronTool.Task
{
    public class SynchronUserToDataCenterTask
    {
        private bool isStart;
        private readonly IDbConnection mDbConnection;
        private readonly int intervalMilliseconds;

        public SynchronUserToDataCenterTask()
        {
            this.mDbConnection = this.BuildDbConnection();
            this.intervalMilliseconds = this.BuildUserTaskConfig().Interval*60*1000;
        }

        private IDbConnection BuildDbConnection()
        {
            var configPath = Path.Combine(AppContext.BaseDirectory, "config/connection.config");
            try
            {
                var config = Utils.GetConfig<DbConnectionConfig>(configPath);
                var connectionBuilder = new SqlConnectionStringBuilder
                {
                    DataSource = config.Server,
                    InitialCatalog = config.Database,
                    ConnectTimeout = config.ConnectTimeout,
                    UserID = config.UserID,
                    Password = config.Password
                };
                var connection = new SqlConnection(connectionBuilder.ConnectionString);
                return connection;
            }
            catch {
            }
            return null;
        }

        private UserTaskConfig BuildUserTaskConfig()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "config/usertask.config");
            var config = Utils.GetConfig<UserTaskConfig>(filePath);
            return config;
        }

        public void Start()
        {
            if (isStart) return;
            isStart = true;
            System.Threading.Tasks.Task.Factory.StartNew(TaskProcess);
        }

        public void Stop()
        {
            if (!isStart) return;
            isStart = false;
        }

        protected void TaskProcess()
        {
            Console.WriteLine("同步用户到中心库任务开始执行……");
            while (isStart)
            {
                //Console.WriteLine("当前时间：{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                Thread.Sleep(this.intervalMilliseconds);
            }
        }
    }
}