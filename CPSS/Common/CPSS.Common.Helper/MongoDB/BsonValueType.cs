namespace CPSS.Common.Helper.MongoDB
{
    public enum BsonValueType : ushort
    {
        BsonArry = 0,
        BsonBinaryData = 1,
        BsonBoolean = 2,
        BsonDateTime = 3,
        BsonDocument = 4,
        BsonDocumentWrapper = 5,
        BsonDouble = 6,
        BsonInt32 = 7,
        BsonInt64 = 8,
        BsonJavaScript = 9,
        BsonJavaScriptWithScope = 10,
        BsonMaxKey = 11,
        BsonMinKey = 12,
        BsonNull = 13,
        BsonObjectId = 14,
        BsonRegularExpression = 15,
        BsonString = 16,
        BsonSymbol = 17,
        BsonTimestamp = 18,
        BsonUndefined = 19,
        LazyBsonArray = 20,
        LazyBsonDocument = 21,
        MaterializedOnDemandBsonArray = 22,
        MaterializedOnDemandBsonDocument = 23,
        RawBsonArray = 24,
        RawBsonDocument = 25
    }
}