using System;
using System.Linq;
using System.Reflection;
using CPSS.Common.Core.Authenticate;
using CPSS.Common.Core.DataAccess.MongoDB.Interface;
using CPSS.Common.Core.DataAccess.MongoDB.Interface.Parameters;
using CPSS.Common.Core.Helper.BuildFilePath;
using CPSS.Common.Core.Helper.Config;
using CPSS.Common.Core.Helper.WebConfig;
using CPSS.Common.Core.MongoDb;
using CPSS.Common.Core.MongoDB;
using CPSS.Common.Core.Paging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CPSS.Common.Core.DataAccess.MongoDB
{
    public class MongoDbBaseDataAccess : IMongoBaseDbDataAccess
    {
        #region Private Field

        private MongoDatabase mMongoDatabase;
        private MongoCollection mMongoCollection;

        #endregion

        #region constructor

        public MongoDbBaseDataAccess()
        {
            var user = CPSSAuthenticate.GetCurrentUser();
            var filePath = "~/config/mongodbconfig.config";
            if (user != null)
            {
                filePath = string.Format("~/config/{0}/mongodbconfig.config",user.CompanySerialNum);
                if (!ExistsFileHelper.ExistsFile(filePath))
                {
                    var config = new MongoDbConfig
                    {
                        Server = WebConfigHelper.GetMongoDbServer(),
                        Database = string.Format("MongoDbLog_{0}", user.CompanySerialNum)
                    };
                    ConfigHelper.Save(config, filePath);
                }
            }
            //if (!ExistsFileHelper.ExistsFile(filePath)) throw new Exception("MongoDb库连接配置文件不存在。");
            var mongoDbConfig = ConfigHelper.GetConfig<MongoDbConfig>(filePath);
            this.InitMongoDataBase(mongoDbConfig.Server, mongoDbConfig.Database);
        }

        #endregion
        
        #region Private Method

        /// <summary>
        /// 初始化数据库MongoDatabase
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="databaseName"></param>
        private void InitMongoDataBase(string connection, string databaseName)
        {
            var mongoClient = new MongoClient(connection);
            var mongoServer = mongoClient.GetServer();
            this.mMongoDatabase = mongoServer.GetDatabase(databaseName);
        }

        /// <summary>
        /// 初始化数据库MongoCollection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private void InitMongoCollection<T>(T data) where T : MogoDbDataEntityConstraint, new()
        {
            var tableName = data.GetType().FullName;
            if (data.SpecialType != null)
                tableName = data.SpecialType.FullName;
            else
            {
                if (!string.IsNullOrEmpty(data.TableName))
                    tableName = data.TableName;
            }
            this.mMongoCollection = this.mMongoDatabase.GetCollection(tableName);
        }

        /// <summary>
        /// 更新含有特殊元数据属性的字段的值(非string类型)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="document"></param>
        /// <param name="data"></param>
        private static void SetSpecialFieldOfValue<T>(BsonDocument document, T data) where T : MogoDbDataEntityConstraint, new()
        {
            var properties = data.GetType().GetProperties().Where(property => property.GetCustomAttribute(typeof(SpecialField)) != null);
            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute(typeof(SpecialField));
                var specialField = attribute as SpecialField;
                if (specialField == null) continue;
                var bsonValueType = specialField.BsonValueType;

                var value = property.GetValue(data);
                switch (bsonValueType)
                {
                    case BsonValueType.BsonDateTime:
                        DateTime _value;
                        if (!DateTime.TryParse(value.ToString(), out _value)) _value = DateTime.Now;
                        document.SetElement(new BsonElement(property.Name, new BsonDateTime(_value)));
                        break;
                }
            }
        }

        /// <summary>
        /// 更新data所有非空值字段的值到document对应字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="document"></param>
        /// <param name="data"></param>
        private static void UpdateAllFieldOfValue<T>(BsonDocument document, T data) where T : MogoDbDataEntityConstraint, new()
        {
            var properties = data.GetType().GetProperties().Where(property => (property.GetValue(data) != null && property.Name.ToLower() != "_id")).ToList();
            foreach (var property in properties)
            {
                var name = property.Name;
                var value = property.GetValue(data).ToString();
                var bsonValue = document.GetElement(name).Value;
                var typeName = bsonValue.GetType().Name;
                if (typeName == BsonValueType.BsonInt32.ToString())
                    document.SetElement(new BsonElement(name, new BsonInt32(int.Parse(value))));
                else if (typeName == BsonValueType.BsonInt64.ToString())
                    document.SetElement(new BsonElement(name, new BsonInt64(long.Parse(value))));
                else if (typeName == BsonValueType.BsonDateTime.ToString())
                    document.SetElement(new BsonElement(name, new BsonDateTime(DateTime.Parse(value))));
                else if (typeName == BsonValueType.BsonDouble.ToString())
                    document.SetElement(new BsonElement(name, new BsonDouble(double.Parse(value))));
                else if (typeName == BsonValueType.BsonString.ToString())
                    document.SetElement(new BsonElement(name, new BsonString(value)));
                else if (typeName == BsonValueType.BsonBoolean.ToString())
                    document.SetElement(new BsonElement(name, new BsonBoolean(bool.Parse(value))));
            }
        }

        #endregion

        #region Public Method

        public bool Save<T>(T data) where T : MogoDbDataEntityConstraint, new()
        {
            try
            {
                this.InitMongoCollection(data);
                var json = JObject.FromObject(data).ToString(Formatting.None);
                var document = BsonDocument.Parse(json);
                document.SetElement(new BsonElement("_id", ObjectId.GenerateNewId()));
                SetSpecialFieldOfValue(document, data);
                var result = this.mMongoCollection.Save(document);
                return result.Ok;
            }
            catch (Exception exception)
            {
                throw new Exception(string.Concat("请先初始化服务器连接(调用InitMongoCollection方法),若已调用InitMongoCollection请检查系统异常。系统异常为：", exception.Message));
            }
        }

        public bool Insert<T>(T data) where T : MogoDbDataEntityConstraint, new()
        {
            try
            {
                this.InitMongoCollection(data);
                var json = JObject.FromObject(data).ToString(Formatting.None);
                var document = BsonDocument.Parse(json);
                document.SetElement(new BsonElement("_id", ObjectId.GenerateNewId()));
                SetSpecialFieldOfValue(document, data);
                var result = this.mMongoCollection.Insert(document);
                return result.Ok;
            }
            catch (Exception exception)
            {
                throw new Exception(string.Concat("请先初始化服务器连接(调用InitMongoCollection方法),若已调用InitMongoCollection请检查系统异常。系统异常为：", exception.Message));
            }
        }

        private BsonDocument GetDataById1<T>(T data) where T : MogoDbDataEntityConstraint, new()
        {
            this.InitMongoCollection(data);
            var query = Query.And(
                Query.EQ("_id", new ObjectId(data.QueryId))
                );
            var document = this.mMongoCollection.FindAs<BsonDocument>(query).FirstOrDefault();
            return document;
        }

        public T GetDataById<T>(T data) where T : MogoDbDataEntityConstraint, new()
        {
            var document = this.GetDataById1(data);
            var result = BsonSerializer.Deserialize<T>(document);
            return result;
        }

        public bool Update<T>(T data) where T : MogoDbDataEntityConstraint, new()
        {
            this.InitMongoCollection(data);
            var document = this.GetDataById1(data);
            UpdateAllFieldOfValue(document, data);
            var result = this.mMongoCollection.Save(document);
            return result.Ok;
        }

        public bool Delete<T>(T data) where T : MogoDbDataEntityConstraint, new()
        {
            this.InitMongoCollection(new T());
            var result = this.mMongoCollection.Remove(Query.EQ("_id", new ObjectId(data.QueryId)), RemoveFlags.None);
            return result.Ok;
        }
        
        public PageData<BsonDocument> GetPageData(MongoDbParameter parameter)
        {
            var recordCount = this.mMongoCollection.Count();
            var resultList =
                this.mMongoCollection.FindAs<BsonDocument>(null)
                    .SetSkip((parameter.PageIndex - 1) * parameter.PageSize)
                    .SetLimit(parameter.PageSize)
                    .Select(document =>
                    {
                        document.Remove("_id");
                        return document;
                    }).ToList();
            var result = new PageData<BsonDocument>(resultList, (int)recordCount);
            return result;
        }

        public PageData<BsonDocument> GetPageDataByCondition(MongoDbParameter parameter)
        {
            if(parameter.MongoQuery == null)
                throw new NotImplementedException("MongoDbParameter的MongoQuery必须使用MongoDb的规定查询条件格式。");

            var cursor = this.mMongoCollection.FindAs<BsonDocument>(parameter.MongoQuery);
            var recordCount = cursor.Count();
            var resultList = cursor.SetSkip((parameter.PageIndex - 1)*parameter.PageSize)
                .SetLimit(parameter.PageSize)
                .Select(document =>
                {
                    document.Remove("_id");
                    return document;
                }).ToList();
            
            var result = new PageData<BsonDocument>(resultList, (int)recordCount);
            return result;
        }

        public PageData<BsonDocument> GetPageDataByConditionHaveID(MongoDbParameter parameter)
        {
            if (parameter.MongoQuery == null)
                throw new NotImplementedException("MongoDbParameter的MongoQuery必须使用MongoDb的规定查询条件格式。");

            var cursor = this.mMongoCollection.FindAs<BsonDocument>(parameter.MongoQuery);
            var resultList = cursor.SetSkip((parameter.PageIndex - 1) * parameter.PageSize)
                .SetLimit(parameter.PageSize)
                .ToList();
            return new PageData<BsonDocument>(resultList, (int)cursor.Count());
        }

        #endregion
    }
}