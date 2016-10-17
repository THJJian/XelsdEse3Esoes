using System;

namespace CPSS.Data.DataAccess.Interfaces.MongoDB.Parameters
{
    public class MongoDbDemoParameter
    {
        public int PageSize { set; get; }

        public int PageIndex { set; get; }

        public string Keyword { set; get; }

        public DateTime CTime { set; get; }
    }
}