using System;
using CPSS.Common.Core.MongoDB;
using Newtonsoft.Json;

namespace CPSS.Common.Core.MongoDb
{
    /// <summary>
    /// MongoDb数据实体约束类，即所有要存放到MongoDb的实体类必须继承自该类
    /// </summary>
    public class MogoDbDataEntityConstraint
    {
        public MogoDbDataEntityConstraint()
        {
            this._id = string.Empty;
            this.SpecialType = null;
            this.TableName = string.Empty;
            this.LogName = "系统日志";
            this.LogData = string.Empty;
            this.LogTime = DateTime.Now;
        }

        /// <summary>
        /// MongoDb默认主键字段
        /// </summary>
        [JsonIgnore]
        public string _id { set; get; }

        /// <summary>
        /// 日志数据存放类。 mongodb的表名优先取值为该值，如果没有指定该值，则取TableName的值；
        /// 如果两者都没有值则取继承自该类的类型的全名称
        /// </summary>
        [JsonIgnore]
        public System.Type SpecialType { set; get; }

        /// <summary>
        /// 日志指定表名。 mongodb的表名优先取值为SpecialType，如果没有指定SpecialType，则取TableName的值；
        /// 如果两者都没有值则取继承自该类的类型的全名称
        /// </summary>
        [JsonIgnore]
        public string TableName { set; get; }

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
        [SpecialField(BsonValueType = BsonValueType.BsonDateTime)]
        public DateTime LogTime { set; get; }
    }
}