using System;
using CPSS.Common.Core.MongoDb;

namespace CPSS.Service.ViewService.ViewModels.MongoDb.Request
{
    public class RequestMongoDbViewModel : MogoDbDataEntityConstraint
    {
        /// <summary>
        /// 日志名称
        /// </summary>
        public string LogName { set; get; }

        /// <summary>
        /// 日志数据，任何对象转换的Json字符串
        /// </summary>
        public string LogData { set; get; }

        /// <summary>
        /// 日志记录日期
        /// </summary>
        public DateTime LogTime { set; get; }

    }
}