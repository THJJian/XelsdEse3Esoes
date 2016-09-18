using System;

namespace CPSS.Common.Core.DataAccess.MongoDB
{
    public class SpecialField : Attribute
    {
        public BsonValueType BsonValueType { set; get; }

    }
}