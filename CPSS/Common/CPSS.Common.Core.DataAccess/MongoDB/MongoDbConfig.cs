using System;

namespace CPSS.Common.Core.DataAccess.MongoDB
{
    [Serializable]
    public class MongoDbConfig
    {
        /// <summary>
        /// 服务器地址(格式：mongodb://127.0.0.1) 
        /// </summary>
        public string Server { set; get; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string Database { set; get; }
    }
}