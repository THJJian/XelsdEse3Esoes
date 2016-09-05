using System;
using System.Linq;
using System.Reflection;
using CPSS.Common.Core.DataAccess.MongoDB;
using CPSS.Common.Core.DataAccess.MongoDB.Interface;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CPSS.Common.Core.Helper.MongoDB.DataAccess
{
    public class MongoDbDataAccess : IMongoDbDataAccess
    {
        private MongoCollection mMongoCollection;

        /// <summary>
        /// 更新含有特殊元数据属性的字段的值(非string类型)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="document"></param>
        /// <param name="data"></param>
        private static void SetSpecialFieldOfValue<T>(BsonDocument document, T data) where T: ConstraintDataEntity, new ()
        {
            var properties = data.GetType().GetProperties().Where(property => property.GetCustomAttribute(typeof(SpecialField)) != null);
            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute(typeof(SpecialField));
                if (attribute == null) continue;
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
        private static void UpdateAllFieldOfValue<T>(BsonDocument document, T data) where T : ConstraintDataEntity, new()
        {
            var properties = data.GetType().GetProperties().Where(property => (property.GetValue(data) != null && property.Name.ToLower() != "_id")).ToList();
            foreach (var property in properties)
            {
                var name = property.Name;
                var value = property.GetValue(data).ToString();
                var bsonValue = document.GetElement(name).Value;
                var typeName = bsonValue.GetType().Name;
                if (typeName == BsonValueType.BsonInt32.ToString())
                    document.SetElement(new BsonElement(name, new BsonInt32(Int32.Parse(value))));
                else if (typeName == BsonValueType.BsonInt64.ToString())
                    document.SetElement(new BsonElement(name, new BsonInt64(Int64.Parse(value))));
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

        public void InitMongoCollection<T>(string connection, string databaseName) where T : ConstraintDataEntity, new()
        {
            if (string.IsNullOrEmpty(databaseName)) throw new Exception("mongoDb数据名称不允许为空。");
            var mongoClient = string.IsNullOrEmpty(connection) ? new MongoClient() : new MongoClient(connection);//如果没有传入连接字符串默认为本地mongoDb服务器
            var mongoServer = mongoClient.GetServer();
            var tableName = typeof(T).FullName;
            var mongoDatabase = mongoServer.GetDatabase(databaseName);
            this.mMongoCollection = mongoDatabase.GetCollection(tableName);
        }

        public bool Save<T>(T data) where T : ConstraintDataEntity, new()
        {
            try
            {
                var json = JObject.FromObject(data).ToString(Formatting.None);
                var document = BsonDocument.Parse(json);
                document.Add(new BsonElement("_id", ObjectId.GenerateNewId()));
                SetSpecialFieldOfValue(document, data);
                var result = this.mMongoCollection.Save(document);
                return result.Ok;
            }
            catch (Exception exception)
            {
                throw new Exception(string.Concat("请先初始化服务器连接(调用InitMongoCollection方法),若已调用InitMongoCollection请检查系统异常。系统异常为：", exception.Message));
            }
        }

        public BsonDocument GetDataById(string id)
        {
            var query = Query.And(
                Query.EQ("_id", new ObjectId(id))
                );
            return this.mMongoCollection.FindAs<BsonDocument>(query).FirstOrDefault();
        }

        public bool Update<T>(T data) where T : ConstraintDataEntity, new()
        {
            var document = this.GetDataById(data._id);
            UpdateAllFieldOfValue(document, data);
            var result = this.mMongoCollection.Save(document);
            return result.Ok;
        }

        public bool Delete(string id)
        {
            var result = this.mMongoCollection.Remove(Query.EQ("_id", new ObjectId(id)), RemoveFlags.None);
            return result.Ok;
        }
    }
}