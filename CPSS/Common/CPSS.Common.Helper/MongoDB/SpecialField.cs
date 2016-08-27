using System;

namespace CPSS.Common.Helper.MongoDB
{
    public class SpecialField : Attribute
    {
        public BsonValueType BsonValueType { set; get; }

    }
}