namespace CPSS.Common.Core.Type.EnumType
{
    public enum CommonDeleted : short
    {
        /// <summary>
        /// 默认包含 NotDeleted、Deleted
        /// </summary>
        Default = 0,
        /// <summary>
        /// 未删除
        /// </summary>
        NotDeleted = 1,
        /// <summary>
        /// 删除
        /// </summary>
        Deleted = 2
    }
}