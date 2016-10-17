using CPSS.Common.Core.DataAccess.MongoDB.Interface.Parameters;
using CPSS.Common.Core.MongoDb;
using CPSS.Common.Core.Paging;
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
        ///     插入数据
        /// </summary>
        /// <param name="data"></param>
        bool Insert<T>(T data) where T : MogoDbDataEntityConstraint, new();

        /// <summary>
        /// 根据ID获取数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        T GetDataById<T>(T data) where T : MogoDbDataEntityConstraint, new();

        /// <summary>
        ///     更新
        /// </summary>
        /// <param name="data"></param>
        bool Update<T>(T data) where T : MogoDbDataEntityConstraint, new();

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="data"></param>
        bool Delete<T>(T data) where T : MogoDbDataEntityConstraint, new();

        /// <summary>
        /// 查询出所有数据(parameter.MongoQuery数据无效)
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        PageData<BsonDocument> GetPageData(MongoDbParameter parameter);

        /// <summary>
        /// 根据插叙条件查询出所有数据。查询的结果不包含主键ID
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        PageData<BsonDocument> GetPageDataByCondition(MongoDbParameter parameter);

        /// <summary>
        /// 根据插叙条件查询出所有数据。查询的结果包含主键ID
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        PageData<BsonDocument> GetPageDataByConditionHaveID(MongoDbParameter parameter);
    }
}