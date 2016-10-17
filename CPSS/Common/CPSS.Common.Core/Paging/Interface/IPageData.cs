namespace CPSS.Common.Core.Paging.Interface
{
    public interface IPageData
    {
        /// <summary>
        /// 记录数量
        /// </summary>
        int DataCount { get; set; }

        /// <summary>
        /// 分页大小
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        int PageIndex { get; set; }
    }
}