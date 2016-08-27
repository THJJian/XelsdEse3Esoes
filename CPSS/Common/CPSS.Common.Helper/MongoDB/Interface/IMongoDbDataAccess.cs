using MongoDB.Bson;

namespace CPSS.Common.Helper.MongoDB.Interface
{
    public interface IMongoDbDataAccess
    {

        /// <summary>
        /// 初始化数据库相关对象 (先调用此方式从配置文件获取配置文件里面的相关配置ConfigHandler.GetConfig<![CDATA[<MongoDbConfig>]]>("~/Config/MongoDbConfig.config", 10));
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="databaseName"></param>
        void InitMongoCollection<T>(string connection, string databaseName) where T : ConstraintDataEntity, new();

        /// <summary>
        ///     保存
        /// </summary>
        /// <param name="data"></param>
        bool Save<T>(T data) where T : ConstraintDataEntity, new();

        /// <summary>
        /// 根据ID获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BsonDocument GetDataById(string id);

        /// <summary>
        ///     更新
        /// </summary>
        /// <param name="data"></param>
        bool Update<T>(T data) where T : ConstraintDataEntity, new();

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="id"></param>
        bool Delete(string id);
    }
}