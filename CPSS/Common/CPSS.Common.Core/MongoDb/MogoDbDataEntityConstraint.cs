namespace CPSS.Common.Core.MongoDb
{
    /// <summary>
    /// MongoDb数据实体约束类，即所有要存放到MongoDb的实体类必须继承自该类
    /// </summary>
    public class MogoDbDataEntityConstraint
    {
        /// <summary>
        /// MongoDb默认主键字段
        /// </summary>
        public string _id { set; get; }
    }
}