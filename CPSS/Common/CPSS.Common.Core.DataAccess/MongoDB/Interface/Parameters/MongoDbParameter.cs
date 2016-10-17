using MongoDB.Driver;

namespace CPSS.Common.Core.DataAccess.MongoDB.Interface.Parameters
{
    public class MongoDbParameter
    {

        public MongoDbParameter()
        {
            this.PageSize = 10;
            this.MongoQuery = null;
        }

        public int PageSize { set; get; }

        public int PageIndex { set; get; }

        /// <summary>
        /// mongodb查询条件
        /// </summary>
        public IMongoQuery MongoQuery { set; get; }
    }
}