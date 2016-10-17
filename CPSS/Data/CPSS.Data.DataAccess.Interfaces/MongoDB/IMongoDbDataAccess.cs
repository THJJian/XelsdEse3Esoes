using System;
using CPSS.Common.Core.DataAccess.MongoDB.Interface;
using CPSS.Common.Core.Paging;
using CPSS.Data.DataAccess.Interfaces.MongoDB.Parameters;

namespace CPSS.Data.DataAccess.Interfaces.MongoDB
{
    public interface IMongoDbDataAccess : IMongoBaseDbDataAccess
    {
        /// <summary>
        /// mongodb查询DEMO
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        PageData<object> GetPageDataDemo(MongoDbDemoParameter parameter);
    }
}