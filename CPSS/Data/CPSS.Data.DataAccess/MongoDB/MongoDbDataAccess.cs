using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CPSS.Common.Core.DataAccess.MongoDB;
using CPSS.Common.Core.DataAccess.MongoDB.Interface.Parameters;
using CPSS.Common.Core.Paging;
using CPSS.Data.DataAccess.Interfaces.MongoDB;
using CPSS.Data.DataAccess.Interfaces.MongoDB.Parameters;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver.Builders;

namespace CPSS.Data.DataAccess.MongoDB
{

    public class MongoDbDataAccess : MongoDbBaseDataAccess,  IMongoDbDataAccess
    {
        public PageData<object> GetPageDataDemo(MongoDbDemoParameter demoParameter)
        {
            var query = Query.And(
                Query.LTE("CTime", new BsonDateTime(demoParameter.CTime)),
                Query.Matches("Keyword", new BsonRegularExpression(new Regex(demoParameter.Keyword, RegexOptions.None)))//mongodb的模糊查询
                );
            var parameter = new MongoDbParameter
            {
                PageIndex = demoParameter.PageIndex,
                PageSize = demoParameter.PageSize,
                MongoQuery = query
            };
            var pagedata = this.GetPageDataByConditionHaveID(parameter);
            var _objs = new List<object>();
            pagedata.Datas.ToList().ForEach(document =>
            {
                var item = BsonSerializer.Deserialize<object>(document);
                _objs.Add(new
                {
                    //Comment = item.Comment,
                    //CTime = item.CTime.ToLocalTime(),
                    //HandleComment = item.HandleComment,
                    //HandleOpinion = item.HandleOpinion,
                    //HandlePerson = item.HandlePerson,
                    //HandleTime = item.HandleTime.ToLocalTime(),
                    //ID = item._id.ToString(),
                    //LinkMan = item.LinkMan,
                    //LinkTel = item.LinkTel,
                    //ProID = item.ProID,
                    //ProMSpec = item.ProMSpec,
                    //ProName = item.ProName,
                    //SearchKeyword = item.SearchKeyword,
                    //SearchType = item.SearchType,
                    //Status = item.Status
                });
            });
            var result = new PageData<object>
            {
                DataCount = pagedata.DataCount,
                Datas = _objs
            };

            return result;
        }
    }
}