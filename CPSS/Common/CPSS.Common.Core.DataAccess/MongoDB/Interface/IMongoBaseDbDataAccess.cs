using CPSS.Common.Core.MongoDb;
using MongoDB.Bson;

namespace CPSS.Common.Core.DataAccess.MongoDB.Interface
{
    public interface IMongoBaseDbDataAccess
    {

        /// <summary>
        ///     保存
        /// </summary>
        /// <param name="data"></param>
        bool Save<T>(T data) where T : MogoDbDataEntityConstraint, new();

        /// <summary>
        /// 根据ID获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BsonDocument GetDataById<T>(string id) where T : MogoDbDataEntityConstraint, new();

        /// <summary>
        ///     更新
        /// </summary>
        /// <param name="data"></param>
        bool Update<T>(T data) where T : MogoDbDataEntityConstraint, new();

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="id"></param>
        bool Delete<T>(string id) where T : MogoDbDataEntityConstraint, new();
    }
}