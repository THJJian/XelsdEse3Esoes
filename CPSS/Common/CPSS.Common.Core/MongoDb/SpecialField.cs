using System;

namespace CPSS.Common.Core.MongoDB
{
    public class SpecialField : Attribute
    {
        public BsonValueType BsonValueType { set; get; }

    }
}